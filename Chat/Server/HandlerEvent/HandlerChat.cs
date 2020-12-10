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
    public class HandlerChat
    {
        #region Fields

        private ConcurrentDictionary<string, ClientProperties> _cachedClientProperies;//Ключ - имя пользователя
        public  ConcurrentDictionary<int, InfoChat> InfoChats { get; }//Ключ - номер комнаты
        private ConcurrentDictionary<int, List<MessageInfo>> _MessagesAtChat;//Ключ - номер комнаты
        private IHandlerRequestToData _data;
        private ITransportServer _server;
		private HandlerConnection _connection;
		//ДОДЕЛАТЬ ЧАТЫ
		#endregion Fields

		public HandlerChat(ITransportServer server, IHandlerRequestToData data, HandlerConnection connection)
        {
			_server = server;

			_server.ConnectedToChat += OnChatOpened;
			_server.AddedChat += OnAddedChat;
			_server.RemovedChat += OnRemovedChat;
			_server.AddedClientsToChat += OnAddedClientsToChat;
			_server.RemovedClientsFromChat += OnRemovedClientsFromChat;

			_data = data;

			_cachedClientProperies = _data.GetInfoAboutLinkClientToChat();
			InfoChats = _data.GetInfoAboutAllChat();

			_connection = connection;
		}

		#region Methods

		public async void OnChatOpened(object sender, ConnectionToChatEventArgs container)
		{
			if (_connection.cachedClientName.TryGetValue(container.NameOfClient, out Guid clientGuid)
			   && InfoChats.TryGetValue(container.NumberChat, out InfoChat infoChat))
			{
				foreach (var nameClientAtChat in infoChat.NameOfClients)
				{
					if (nameClientAtChat == container.NameOfClient)
					{
						if (_MessagesAtChat.TryGetValue(container.NumberChat, out List<MessageInfo> messages) && clientGuid != Guid.Empty)
						{
							await Task.Run(() => _server.Send(new List<Guid>() { clientGuid }, new ConnectToChatResponse(container.NumberChat, messages).GetContainer()));
						}
					}
				}
			}
			else
			{
				//Ошибка
				return;
			}
		}
		//ДОДЕЛАТЬ ЧАТЫ
		public async void OnAddedChat(object sender, AddedChatEventArgs container)
		{
			if (_cachedClientProperies.TryGetValue(container.NameOfClientSender, out ClientProperties clientSenderProperties))
			{
				int numberChat = await Task.Run(() => _data.CreatNewChat(new CreatingChatInfo { NameOfClientSender = container.NameOfClientSender, NameOfClients = container.NameOfClientsForAdd }));
				if (numberChat != -1)
				{
					List<Guid> idClientsForSendMessage = new List<Guid>();//Создание списка id для рассылки им сообщений
					foreach (var nameClient in container.NameOfClientsForAdd)
					{
						if (_cachedClientProperies.TryGetValue(nameClient, out ClientProperties clientOfChat))
						{
							if (clientOfChat.IdConnection != Guid.Empty)
							{
								idClientsForSendMessage.Add(clientOfChat.IdConnection);
							}
							var lastValueChatsClient = clientOfChat;
							clientOfChat.NumbersChat.Add(numberChat);
							_cachedClientProperies.TryUpdate(nameClient, clientOfChat, lastValueChatsClient);
						}
					}
					if (clientSenderProperties.IdConnection != Guid.Empty)
					{
						idClientsForSendMessage.Add(clientSenderProperties.IdConnection);
					}

					await Task.Run(() => _server.Send(idClientsForSendMessage, new AddNewChatResponse(numberChat, container.NameOfClientsForAdd).GetContainer()));
					InfoChats.TryAdd(numberChat, new InfoChat { OwnerChat = container.NameOfClientSender, NameOfClients = container.NameOfClientsForAdd });
				}
				else
				{
					//Ошибка добавления
				}
			}
			else
			{
				//Ошибка, не существует клиента
			}
		}
		public async void OnRemovedChat(object sender, RemovedChatEventArgs container)
		{
			if (_cachedClientProperies.TryGetValue(container.NameOfClient, out ClientProperties clientProperties)
				&& InfoChats.TryGetValue(container.NumberChat, out InfoChat infoChat))
			{
				if (container.NameOfClient == infoChat.OwnerChat)
				{
					List<Guid> idClientsForSendMessage = new List<Guid>();//Создание списка id для рассылки им сообщений
					foreach (var nameClient in infoChat.NameOfClients)
					{
						if (_cachedClientProperies.TryGetValue(nameClient, out ClientProperties clientOfChat) && clientOfChat.IdConnection != Guid.Empty)
						{
							idClientsForSendMessage.Add(clientOfChat.IdConnection);
						}
						var lastValueChatsClient = clientOfChat;
						clientOfChat.NumbersChat.Remove(container.NumberChat);
						_cachedClientProperies.TryUpdate(nameClient, clientOfChat, lastValueChatsClient);
					}

					await Task.Run(() => _server.Send(idClientsForSendMessage, new RemoveChatResponse(container.NumberChat).GetContainer()));

					if (!InfoChats.TryRemove(container.NumberChat, out InfoChat infoRemovedChat))
					{
						//Не нашел такую комнату
					}

					if (!await Task.Run(() => _data.RemoveChat(container.NumberChat)))
					{
						//Ошибка на удаление в БД
					}
				}
			}
			else
			{
				//Ошибка
				return;
			}
		}

		public async void OnAddedClientsToChat(object sender, AddedClientsToChatEventArgs container)
		{
			if (_cachedClientProperies.TryGetValue(container.NameOfClientSender, out ClientProperties clientProperties)
				&& InfoChats.TryGetValue(container.NumberChat, out InfoChat infoChat))
			{
				if (container.NameOfClientSender == infoChat.OwnerChat)
				{
					InfoChat buffer = infoChat;
					List<Guid> idClientsForSendMessage = new List<Guid>();//Создание списка id для рассылки им сообщений


					foreach (var nameClient in container.NameOfClients)
					{
						if (_cachedClientProperies.TryGetValue(nameClient, out ClientProperties clientOfChat))
						{
							if (clientOfChat.IdConnection != Guid.Empty)
							{
								idClientsForSendMessage.Add(clientOfChat.IdConnection);
							}
							var lastValueChatsClient = clientOfChat;
							clientOfChat.NumbersChat.Add(container.NumberChat);
							_cachedClientProperies.TryUpdate(nameClient, clientOfChat, lastValueChatsClient);
						}
						else
						{
							container.NameOfClients.Remove(nameClient);
						}
					}

					infoChat.NameOfClients.Union(container.NameOfClients);
					foreach (var nameClient in infoChat.NameOfClients)
					{
						if (_cachedClientProperies.TryGetValue(nameClient, out ClientProperties clientOfChat))
						{
							if (clientOfChat.IdConnection != Guid.Empty)
							{
								idClientsForSendMessage.Add(clientOfChat.IdConnection);
							}
						}
					}

					await Task.Run(() => _server.Send(idClientsForSendMessage, new AddNewClientToChatResponse(container.NameOfClientSender, container.NameOfClients, container.NumberChat).GetContainer()));

					InfoChats.TryUpdate(container.NumberChat, infoChat, buffer);

					if (!await Task.Run(() => _data.AddClientToChat(new AddClientToChat { NumberChat = container.NumberChat, NameOfClients = container.NameOfClients })))
					{
						//Ошибка на добавление в БД
					}
				}
			}
			else
			{
				//Ошибка
				return;
			}
		}
		public async void OnRemovedClientsFromChat(object sender, RemovedClientsFromChatEventArgs container)
		{
			if (_cachedClientProperies.TryGetValue(container.NameOfClient, out ClientProperties clientProperties)
				&& InfoChats.TryGetValue(container.NumberChat, out InfoChat infoChat))
			{
				if (container.NameOfClient == infoChat.OwnerChat)
				{
					InfoChat buffer = infoChat;
					List<Guid> idClientsForSendMessage = new List<Guid>();//Создание списка id для рассылки им сообщений
					foreach (var nameClient in infoChat.NameOfClients)
					{
						if (_cachedClientProperies.TryGetValue(nameClient, out ClientProperties clientOfChat) && clientOfChat.IdConnection != Guid.Empty)
						{
							idClientsForSendMessage.Add(clientOfChat.IdConnection);
						}
					}

					foreach (var nameClientForRemove in container.NameOfClients)
					{
						infoChat.NameOfClients.Remove(nameClientForRemove);
						var lastValueChatsClient = clientProperties;
						clientProperties.NumbersChat.Remove(container.NumberChat);
						_cachedClientProperies.TryUpdate(nameClientForRemove, clientProperties, lastValueChatsClient);
					}

					await Task.Run(() => _server.Send(idClientsForSendMessage, new RemoveClientFromChatResponse(container.NameOfClient, container.NameOfClients, container.NumberChat).GetContainer()));

					InfoChats.TryUpdate(container.NumberChat, infoChat, buffer);

					if (!await Task.Run(() => _data.RemoveClientFromChat(new RemoveClientFromChat { NumberChat = container.NumberChat, NameOfClients = container.NameOfClients })))
					{
						//Ошибка на удаление в БД
					}
				}
			}
			else
			{
				//Ошибка
				return;
			}
		}


		#endregion Methods
	}
}
