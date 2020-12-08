namespace Common.Network
{
	using System;
	using System.Collections.Generic;
	using System.Collections.Concurrent;
	using System.Threading.Tasks;
	using Packets;

	public class HandlerRequestFromClient
	{
		#region Fields

		private ConcurrentDictionary<string,ClientProperties> _cachedClientProperies;//Ключ - имя пользователя
		private ConcurrentDictionary<int, InfoChat> _infoAllChat;//Ключ - номер комнаты
		private ConcurrentDictionary<int, List<MessageInfo>> _MessagesAtChat;//Ключ - номер комнаты
		private IGetOrSetData _data;
		private ITransportServer _server;

		#endregion Fields

		#region Constructors

		public HandlerRequestFromClient(ITransportServer server, IGetOrSetData data)
		{
			_server = server;

			_server.ClientConnected += OnConnect;
			_server.ClientDisconnected += OnDisconnect;
			_server.MessageReceived += OnMessage;
			_server.ConnectedToChat += OnChatOpened;
			_server.AddedChat += OnAddedChat;
			_server.RemovedChat += OnRemovedChat;
			_server.AddedClientsToChat += OnAddedClientsToChat;
			_server.RemovedClientsFromChat += OnRemovedClientsFromChat;

			_data = data;

			_cachedClientProperies = _data.GetInfoAboutAllClient();
			_infoAllChat = _data.GetInfoAboutAllChat();
			_MessagesAtChat = _data.GetAllMessageFromChats();
		}

		#endregion Constructors

		#region Methods

		public async void OnConnect(object sender, ClientConnectedEventArgs container)
		{
			if (_cachedClientProperies.TryGetValue(container.ClientName,out ClientProperties clientProperties))
			{
				if(clientProperties.IdConnection == Guid.Empty)
				{
					await Task.Run(() => _server.Send(new List<Guid>() { clientProperties.IdConnection }, new ConnectionResponse(ResultRequest.Ok, "Пользователь подключен", clientProperties.NumbersChat).GetContainer()));
					clientProperties.IdConnection = container.ClientId;
				}
				else
				{
					await Task.Run(() => _server.Send(new List<Guid>() { clientProperties.IdConnection }, new ConnectionResponse(ResultRequest.Failure, "Такой пользователь уже есть", new List<int>()).GetContainer()));
					await Task.Run(() => _server.FreeConnection(clientProperties.IdConnection));
				}
			}
			else
			{
				await Task.Run(() => _server.Send(new List<Guid>() { clientProperties.IdConnection }, new ConnectionResponse(ResultRequest.Ok, "Новый пользователь зарегистрирован",clientProperties.NumbersChat).GetContainer()));
				_cachedClientProperies.TryAdd(container.ClientName, new ClientProperties { IdConnection = container.ClientId, NumbersChat = new List<int>() });
				
				if(!await Task.Run(() => _data.AddNewClient(new ClientInfo { NameOfClient = container.ClientName})))
				{
					//Ошибка, не получилось записать
				}
			}
		}

		/*private async Task ClientNotice(object sender, string nameClientConnected)//Доделать:рассылку другим пользователям, что зашел пользователь
		{
			List<int> ChatForNotice = new List<int>();
			if(_cachedClientProperies.TryGetValue(nameClientConnected, out ClientProperties clientProperties))
            {
				foreach(var numberChat in clientProperties.NumbersChat)
                {
					if(_infoAllChat.TryGetValue(numberChat, out InfoChat infoChat))
                    {
						foreach(var nameClient in infoChat.NameClients)
                        {
							if(_cachedClientProperies.TryGetValue(nameClient, out ClientProperties clientPropertiesForNotice) && clientPropertiesForNotice.IdConnection!= Guid.Empty)
                            {
								List.Intersect(clientPropertiesForNotice.NumbersChat, clientProperties.NumbersChat);
								ChatForNotice.Add(clientPropertiesForNotice.IdConnection);
								await Task.Run(() => _server.Send(clientsForNotice,new ConnectionNoticeForClients().GetContainer()));
							}	
						}
					}
                }
            }
		}*/

		public async void OnDisconnect(object sender, ClientDisconnectedEventArgs container)
		{
			if (_cachedClientProperies.TryGetValue(container.NameOfClient,out ClientProperties clientProperties) && clientProperties.IdConnection != Guid.Empty)
			{
				await Task.Run(() => _server.FreeConnection(clientProperties.IdConnection));
				_cachedClientProperies.TryUpdate(container.NameOfClient, new ClientProperties{ IdConnection = Guid.Empty, NumbersChat = clientProperties.NumbersChat }, clientProperties);
			}
		}

		public async void OnMessage(object sender, MessageReceivedEventArgs container)
		{
			if(_cachedClientProperies.ContainsKey(container.NameOfClient)
			   && _infoAllChat.TryGetValue(container.NumberChat, out InfoChat infoChat))
			{
				List<Guid> idClientsForSendMessage = new List<Guid>();//Создание списка id для рассылки им сообщений
				foreach (var nameClient in infoChat.NameClients)
				{
					if (_cachedClientProperies.TryGetValue(nameClient, out ClientProperties clientProperties) && clientProperties.IdConnection != Guid.Empty)
					{
						idClientsForSendMessage.Add(clientProperties.IdConnection);
					}
				}
				await Task.Run(() =>  _server.Send(idClientsForSendMessage, new MessageResponse(container.NameOfClient, container.Message, container.NumberChat).GetContainer()));

				DateTime time = DateTime.Now;
				if (_MessagesAtChat.ContainsKey(container.NumberChat))
				{
					_MessagesAtChat[container.NumberChat].Add(new MessageInfo { FromMessage = container.NameOfClient, Text = container.Message, Time = time });
				}

				if (!await Task.Run(() => _data.AddNewMessage(new MessageInfoForDb { NumberChat = container.NumberChat, 
																					 FromMessage = container.NameOfClient, 
																					 Text = container.Message, Time = time })))
				{
					//Сообщение не удалось добавить, сигнал серверу на запрет приема сообщений до добавления сообщения
				}
			}
			else
			{
				//Ошибка, не существует либо пользователя, либо комнаты
			}
		}

		public async void OnChatOpened(object sender, ConnectionToChatEventArgs container)
		{
			if(_cachedClientProperies.TryGetValue(container.NameOfClient,out ClientProperties clientProperties)
			   && _infoAllChat.TryGetValue(container.NumberChat, out InfoChat infoChat))
			{
				foreach(var nameClientAtChat in infoChat.NameClients)
				{
					if(nameClientAtChat == container.NameOfClient)
					{
						if(_MessagesAtChat.TryGetValue(container.NumberChat, out List<MessageInfo> messages) && clientProperties.IdConnection != Guid.Empty)
						{
							await Task.Run(() => _server.Send(new List<Guid>() { clientProperties.IdConnection }, new ConnectToChatResponse(container.NumberChat, messages).GetContainer()));
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

		public async void OnAddedChat(object sender, AddedChatEventArgs container)
		{
			if (_cachedClientProperies.TryGetValue(container.NameOfClientSender, out ClientProperties clientProperties))
			{
				List<Guid> idClientsForSendMessage = new List<Guid>();//Создание списка id для рассылки им сообщений
				foreach (var nameClient in container.NameOfClientsForAdd)
				{
					if (_cachedClientProperies.TryGetValue(nameClient, out ClientProperties clientOfChat) && clientOfChat.IdConnection != Guid.Empty)
					{
						idClientsForSendMessage.Add(clientOfChat.IdConnection);
						container.NameOfClientsForAdd.Remove(nameClient);
					}
				}

				int numberChat = await Task.Run(() => _data.CreatNewChat(new CreatingChatInfo { NameOfClientSender = container.NameOfClientSender, NameOfClients = container.NameOfClientsForAdd }));
				if(numberChat != -1)
				{
					await Task.Run(() => _server.Send(idClientsForSendMessage, new AddNewChatResponse(numberChat, container.NameOfClientsForAdd).GetContainer()));
					_infoAllChat.TryAdd(numberChat, new InfoChat{ OwnerChat = container.NameOfClientSender, NameClients = container.NameOfClientsForAdd });
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
				&& _infoAllChat.TryGetValue(container.NumberChat,out InfoChat infoChat))
			{
				if(container.NameOfClient == infoChat.OwnerChat)
				{
					List<Guid> idClientsForSendMessage = new List<Guid>();//Создание списка id для рассылки им сообщений
					foreach (var nameClient in infoChat.NameClients)
					{
						if (_cachedClientProperies.TryGetValue(nameClient, out ClientProperties clientOfChat) && clientOfChat.IdConnection != Guid.Empty)
						{
							idClientsForSendMessage.Add(clientOfChat.IdConnection);
						}
					}

					if (!_infoAllChat.TryRemove(container.NumberChat, out InfoChat infoRemovedChat))
                    {
						//Не нашел такую комнату
                    }

					await Task.Run(() => _server.Send(idClientsForSendMessage, new RemoveChatResponse(container.NumberChat).GetContainer()));
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
				&& _infoAllChat.TryGetValue(container.NumberChat, out InfoChat infoChat))
			{
				if(container.NameOfClientSender == infoChat.OwnerChat)
				{
					List<Guid> idClientsForSendMessage = new List<Guid>();//Создание списка id для рассылки им сообщений
					foreach (var nameClient in container.NameOfClients)
					{
						if (_cachedClientProperies.TryGetValue(nameClient, out ClientProperties clientOfChat) && clientOfChat.IdConnection != Guid.Empty)
						{
							idClientsForSendMessage.Add(clientOfChat.IdConnection);
							InfoChat buffer = infoChat;
							infoChat.NameClients.Add(nameClient);
							_infoAllChat.TryUpdate(container.NumberChat, infoChat, buffer);
						}
					}

					await Task.Run(() => _server.Send(idClientsForSendMessage, new AddNewClientToChatResponse(container.NameOfClientSender, container.NameOfClients, container.NumberChat).GetContainer()));
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
			if (_cachedClientProperies.ContainsKey(container.NameOfClient)
				&& _infoAllChat.TryGetValue(container.NumberChat, out InfoChat infoChat))
			{
				if (container.NameOfClient == infoChat.OwnerChat)
				{
					List<Guid> idClientsForSendMessage = new List<Guid>();//Создание списка id для рассылки им сообщений
					foreach (var nameClient in infoChat.NameClients)
					{
						if (_cachedClientProperies.TryGetValue(nameClient, out ClientProperties clientOfChat) && clientOfChat.IdConnection != Guid.Empty)
						{
							idClientsForSendMessage.Add(clientOfChat.IdConnection);
							_infoAllChat[container.NumberChat].NameClients.Remove(nameClient);
						}
					}

					await Task.Run(() => _server.Send(idClientsForSendMessage, new RemoveClientFromChatResponse(container.NameOfClient, container.NameOfClients, container.NumberChat).GetContainer()));
					
					if (!await Task.Run(() => _data.RemoveClientFromChat(new RemoveClientFromChat {NumberChat = container.NumberChat, NameOfClients = container.NameOfClients})))
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
