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

		#region Fields

		public ConcurrentDictionary<string, Guid> cachedClientName { get; }//Ключ - имя пользователя
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
					await Task.Run(() => _server.Send(new List<Guid>() { clientGuid },
										 new ConnectionResponse(ResultRequest.Ok, "Пользователь подключен",
																new Dictionary<int, string>(),
																new Dictionary<string, bool>()).GetContainer()));
					clientGuid = container.ClientId;
					await Task.Run(() => _server.SendAll(new ConnectionNoticeForClients(container.ClientName).GetContainer()));
				}
				else
				{
					await Task.Run(() => _server.Send(new List<Guid>() { clientGuid },
										 new ConnectionResponse(ResultRequest.Failure, "Такой пользователь уже есть",
																new Dictionary<int, string>(),
																new Dictionary<string, bool>()).GetContainer()));
					await Task.Run(() => _server.FreeConnection(clientGuid));
				}
			}
			else
			{
				//Первый аргумент номер комнаты, второй словарь клиентов и их активность
				Dictionary<string, bool> chatInfoForClient = new Dictionary<string, bool>();
				if (_chats.InfoChats.TryGetValue(NumberGeneralChat, out InfoChat infoGeneralChat))
				{
					foreach (var nameClientAtChat in infoGeneralChat.NameOfClients)
					{
						if (cachedClientName.TryGetValue(nameClientAtChat, out Guid clientAtChat))
						{
							if (clientAtChat != Guid.Empty)
							{
								chatInfoForClient.Add(nameClientAtChat, true);
							}
							else
							{
								chatInfoForClient.Add(nameClientAtChat, false);
							}
						}
					}
				}
				await Task.Run(() => _server.Send(new List<Guid>() { clientGuid },
									 new ConnectionResponse(ResultRequest.Ok, "Новый пользователь зарегистрирован",
															new Dictionary<int, string>() { { NumberGeneralChat, infoGeneralChat.OwnerChat } },
															chatInfoForClient).GetContainer()));

				cachedClientName.TryAdd(container.ClientName,  container.ClientId);

				await Task.Run(() => _server.SendAll(new ConnectionNoticeForClients(container.ClientName).GetContainer()));

				if (!await Task.Run(() => _data.AddNewClient(new ClientInfo { NameOfClient = container.ClientName })))
				{
					//Ошибка, не получилось записать
				}
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

		public async void OnDisconnect(object sender, ClientDisconnectedEventArgs container)
		{
			if (cachedClientName.TryGetValue(container.NameOfClient, out Guid clientGuid) && clientGuid != Guid.Empty)
			{
				await Task.Run(() => _server.FreeConnection(clientGuid));

				await Task.Run(() => _server.SendAll(new DisconnectRequest(container.NameOfClient).GetContainer()));

				cachedClientName.TryUpdate(container.NameOfClient, Guid.Empty, clientGuid);
			}
		}

	}
}
