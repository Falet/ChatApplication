using Common.Network;
using Common.Network.Packets;
using Server.DataBase;
using Server.Network;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Server.HandlerEvent
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

		public HandlerConnection(ITransportServer server, IHandlerRequestToData data, HandlerChat chats)
        {
            _server = server;

            _server.ClientConnected += OnConnect;
            _server.ClientDisconnected += OnDisconnect;

            _data = data;

            cachedClientName = _data.GetInfoAboutAllClient();
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
					_server.SendAll(Container.GetContainer(nameof(DisconnectRequest), new DisconnectRequest(container.NameOfClient)))
				);

				cachedClientName.TryUpdate(container.NameOfClient, Guid.Empty, clientGuid);
			}
		}

		//Если не будет существовать общего чата, то нужно будет рассылать сообщение о подключении только тем пользователям, 
		//которые находятся в одном чате с подключившимся
		/*private async Task ClientNotice(string nameClientConnected)//Доделать:рассылку другим пользователям, что зашел пользователь
		{
			List<Guid> ChatForNotice = new List<Guid>();
			if(_cachedClientProperies.TryGetValue(nameClientConnected, out ClientProperties clientProperties))
            {
				foreach(var numberChat in clientProperties.NumbersChat)
                {
					if(_infoAllChat.TryGetValue(numberChat, out InfoChat infoChat))
                    {
						foreach(var nameClient in infoChat.NameClients)
                        {
							if(_cachedClientProperies.TryGetValue(nameClient, out ClientProperties clientPropertiesForNotice) && clientPropertiesForNotice.IdConnection != Guid.Empty)
                            {
								ChatForNotice.Add(clientPropertiesForNotice.IdConnection);
							}	
						}
					}
                }
				var DistinctListClients = ChatForNotice.Distinct().ToList();
				await Task.Run(() => _server.Send(DistinctListClients, new ConnectionNoticeForClients(nameClientConnected).GetContainer()));
			}
		}*/
	}
}
