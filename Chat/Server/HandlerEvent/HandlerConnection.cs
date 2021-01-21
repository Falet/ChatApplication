namespace Server.Network
{
	using Common.Network;
	using Common.Network.Packets;
	using Server.DataBase;
	using System;
	using System.Collections.Concurrent;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	public class HandlerConnection
    {
		#region Const

		const int NumberGeneralChat = 1;

		#endregion Const

		#region Properties

		public ConcurrentDictionary<string, Guid> cachedClientName { get; }//Ключ - имя пользователя

		#endregion Properties

		#region Fields

		private IHandlerRequestToData _data;
        private ITransportServer _server;

		#endregion Fields

		#region Constructors

		public HandlerConnection(ITransportServer server, IHandlerRequestFromClient handlerRequestFromClient, IHandlerRequestToData data)
		{
			_server = server;

			handlerRequestFromClient.ClientConnected += OnClientConnected;
			handlerRequestFromClient.ClientDisconnected += OnClientDisconnected;
			handlerRequestFromClient.RequestInfoAllClient += OnRequestInfoAllClient;

			_data = data;
			cachedClientName = _data.GetInfoAboutAllClient();
		}

		#endregion Constructors

		#region Methods

		public async void OnClientConnected(object sender, ClientConnectedEventArgs container)
		{
			if(container.ClientName == "Server")
            {
				var SendMessageToClient = Task.Run(() =>
					_server.Send(new List<Guid>() { container.ClientId },
								 Container.GetContainer(nameof(ConnectionResponse),
														new ConnectionResponse(ResultRequest.Failure, "Запрещенное имя")))
					);
				return;
			}

			if (cachedClientName.TryGetValue(container.ClientName, out Guid clientGuid))
			{
				if (clientGuid == Guid.Empty)
				{
					cachedClientName.TryUpdate(container.ClientName, container.ClientId, Guid.Empty);

					var SendMessage = Task.Run(() =>
					_server.Send(new List<Guid> { container.ClientId }, Container.GetContainer(nameof(ConnectionResponse), new ConnectionResponse(ResultRequest.Ok, container.ClientName)))
					);
					var SendMessageAll = Task.Run(() =>
					_server.SendAll(container.ClientId, Container.GetContainer(nameof(ConnectionNotice), new ConnectionNotice(container.ClientName)))
					);

					_server.SetLoginConnection(container.ClientId, container.ClientName);
				}
				else
				{
					await Task.Run(() =>
					_server.Send(new List<Guid>() { container.ClientId },
								 Container.GetContainer(nameof(ConnectionResponse),
														new ConnectionResponse(ResultRequest.Failure, "Такой пользователь уже есть")))
					);

					_server.FreeConnection(container.ClientId);
				}
			}
			else
			{
				cachedClientName.TryAdd(container.ClientName, container.ClientId);

				var SendMessage = Task.Run(() =>
					_server.Send(new List<Guid> { container.ClientId }, Container.GetContainer(nameof(ConnectionResponse), new ConnectionResponse(ResultRequest.Ok, container.ClientName)))
					);
				var SendMessageAll = Task.Run(() =>
					_server.SendAll(container.ClientId, Container.GetContainer(nameof(ConnectionNotice), new ConnectionNotice(container.ClientName)))
				);

				_server.SetLoginConnection(container.ClientId, container.ClientName);

				if (!await Task.Run(() => _data.AddNewClient(new ClientInfo { NameClient = container.ClientName })))
				{
					//Ошибка, не получилось записать
				}
			}
		}
		public void OnClientDisconnected(object sender, ClientDisconnectedEventArgs container)
		{
			if (cachedClientName.TryGetValue(container.NameClient, out Guid clientGuid))
			{
				var SendMessageToClient = Task.Run(() =>
					_server.SendAll(Guid.Empty, Container.GetContainer(nameof(DisconnectNotice), new DisconnectNotice(container.NameClient)))
				);

				cachedClientName.TryUpdate(container.NameClient, Guid.Empty, clientGuid);

				_server.FreeConnection(container.NameGuid);
			}
		}
		public void OnRequestInfoAllClient(object sender, InfoAboutAllClientsEventArgs container)
		{
			if (cachedClientName.TryGetValue(container.NameClient, out Guid clientGuid))
			{
				Dictionary<string, bool> ActivityClient = new Dictionary<string, bool>();
				foreach (var item in cachedClientName)
				{
					if (item.Value == Guid.Empty)
					{
						ActivityClient.Add(item.Key, false);
					}
					else
					{
						ActivityClient.Add(item.Key, true);
					}
				}
				ActivityClient.Remove(container.NameClient);
				var SendMessage = Task.Run(() =>
				_server.Send(new List<Guid> { clientGuid }, Container.GetContainer(nameof(InfoAboutAllClientsResponse),
																					new InfoAboutAllClientsResponse(ActivityClient)))
				);
			}
		}

		#endregion Methods

	}
}
