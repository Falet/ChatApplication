using Common.Network;
using Common.Network.Packets;
using Server.DataBase;
using Server.Network;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Server.Network
{
    public class HandlerConnection
    {

		#region Const

		const int NumberGeneralChat = 1;

		#endregion Const

		public ConcurrentDictionary<string, Guid> cachedClientName { get; }//Ключ - имя пользователя

		#region Fields


		private IHandlerRequestToData _data;
        private ITransportServer _server;

		#endregion Fields

		public HandlerConnection(ITransportServer server, IHandlerRequestFromClient handlerRequestFromClient, IHandlerRequestToData data)
        {
            _server = server;

			handlerRequestFromClient.ClientConnected += OnConnect;
			handlerRequestFromClient.ClientDisconnected += OnDisconnect;
			handlerRequestFromClient.RequestInfoAllClient += OnClientInfo;

            _data = data;
            cachedClientName = _data.GetInfoAboutAllClient();
		}
		public async void OnConnect(object sender, ClientConnectedEventArgs container)
		{
			if (cachedClientName.TryGetValue(container.ClientName, out Guid clientGuid))
			{
				if (clientGuid == Guid.Empty)
				{
					cachedClientName.TryUpdate(container.ClientName, container.ClientId, Guid.Empty);

					var SendMessage = Task.Run(() =>
					_server.Send(new List<Guid> { container.ClientId },Container.GetContainer(nameof(ConnectionResponse), new ConnectionResponse(ResultRequest.Ok,container.ClientName)))
					);
					var SendMessageAll = Task.Run(() =>
					_server.SendAll(container.ClientId, Container.GetContainer(nameof(ConnectionNoticeForClients), new ConnectionNoticeForClients(container.ClientName)))
					);

					_server.SetLoginConnection(container.ClientId, container.ClientName);
				}
				else
				{
					var SendMessageToServer = Task.Run(() =>
					_server.Send(new List<Guid>() { container.ClientId },
								 Container.GetContainer(nameof(ConnectionResponse), 
														new ConnectionResponse(ResultRequest.Failure, "Такой пользователь уже есть")))
					);
				}
			}
			else
			{
				cachedClientName.TryAdd(container.ClientName,  container.ClientId);

				var SendMessage = Task.Run(() =>
					_server.Send(new List<Guid> { container.ClientId }, Container.GetContainer(nameof(ConnectionResponse), new ConnectionResponse(ResultRequest.Ok, container.ClientName)))
					);
				var SendMessageAll = Task.Run(() =>
					_server.SendAll(container.ClientId, Container.GetContainer(nameof(ConnectionNoticeForClients), new ConnectionNoticeForClients(container.ClientName)))
				);

				_server.SetLoginConnection(container.ClientId, container.ClientName);

				if (!await Task.Run(() => _data.AddNewClient(new ClientInfo { NameOfClient = container.ClientName })))
				{
					//Ошибка, не получилось записать
				}
			}
		}
		public void OnDisconnect(object sender, ClientDisconnectedEventArgs container)
		{
			if (cachedClientName.TryGetValue(container.NameClient, out Guid clientGuid))
			{
				var SendMessageToServer = Task.Run(() =>
					_server.SendAll(clientGuid, Container.GetContainer(nameof(DisconnectNotice), new DisconnectNotice(container.NameClient)))
				);

				cachedClientName.TryUpdate(container.NameClient, Guid.Empty, clientGuid);
			}
		}
		public void OnClientInfo(object sender, InfoAboutAllClientsEventArgs container)
        {
			if(cachedClientName.TryGetValue(container.NameClient, out Guid clientGuid))
			{
				Dictionary<string, bool> ActivityClient = new Dictionary<string, bool>();
				foreach (var item in cachedClientName)
				{
					if(item.Value == Guid.Empty)
                    {
						ActivityClient.Add(item.Key,false);
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
	}
}
