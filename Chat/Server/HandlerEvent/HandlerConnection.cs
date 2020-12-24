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
		private HandlerChat _chats;

		#endregion Fields

		public HandlerConnection(ITransportServer server, IHandlerRequestToData data)
        {
            _server = server;

            _server.ClientConnected += OnConnect;
            _server.ClientDisconnected += OnDisconnect;

            _data = data;
            cachedClientName = _data.GetInfoAboutAllClient();
		}
		public void AddChats(HandlerChat chats)
        {
			_chats = chats;
		}
		public async void OnConnect(object sender, ClientConnectedEventArgs container)
		{
			if (cachedClientName.TryGetValue(container.ClientName, out Guid clientGuid))
			{
				if (clientGuid == Guid.Empty)
				{
					clientGuid = container.ClientId;
					var SendMessageToServer = Task.Run(() =>
					_server.SendAll(Container.GetContainer(nameof(ConnectionNoticeForClients), new ConnectionNoticeForClients(container.ClientName)))
					);
				}
				else
				{
					var SendMessageToServer = Task.Run(() =>
					_server.Send(new List<Guid>() { clientGuid },
								 Container.GetContainer(nameof(ConnectionResponse), 
														new ConnectionResponse(ResultRequest.Failure, "Такой пользователь уже есть")))
					);
					var SendMessageDisconnectToServer = Task.Run(() => _server.FreeConnection(clientGuid));
				}
			}
			else
			{
				cachedClientName.TryAdd(container.ClientName,  container.ClientId);

				var SendMessageToServer = Task.Run(() =>
					_server.SendAll(Container.GetContainer(nameof(ConnectionNoticeForClients), new ConnectionNoticeForClients(container.ClientName)))
				);

				if (!await Task.Run(() => _data.AddNewClient(new ClientInfo { NameOfClient = container.ClientName })))
				{
					//Ошибка, не получилось записать
				}
			}
		}
		public void OnDisconnect(object sender, ClientDisconnectedEventArgs container)
		{
			if (cachedClientName.TryGetValue(container.NameOfClient, out Guid clientGuid) && clientGuid != Guid.Empty)
			{
				var SendMessageDisconnectToServer = Task.Run(() => _server.FreeConnection(clientGuid));

				var SendMessageToServer = Task.Run(() =>
					_server.SendAll(Container.GetContainer(nameof(DisconnectNotice), new DisconnectNotice(container.NameOfClient)))
				);

				cachedClientName.TryUpdate(container.NameOfClient, Guid.Empty, clientGuid);
			}
		}
	}
}
