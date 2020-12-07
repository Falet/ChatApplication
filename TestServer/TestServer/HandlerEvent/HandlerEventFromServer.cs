using System;
using System.Collections.Generic;
using System.Text;

namespace TestServer.Network
{
    using System.Collections.Concurrent;
    using System.Threading;
	using System.Threading.Tasks;
	public class HandlerRequestFromServer
	{
		#region Fields

		private ConcurrentDictionary<string,UserProperties> _usersProperties;//Ключ - имя пользователя
		private ConcurrentDictionary<int, InfoRoom> _infoAllRoom;//Ключ - номер комнаты
		private ConcurrentDictionary<int, List<MessageInfo>> _MessageAtChat;//Ключ - номер комнаты
		private IGetOrSetData _data;
		private ITransport _server;

		#endregion Fields

		public HandlerRequestFromServer(ITransport server, IGetOrSetData data)
		{
			_server = server;

			_server.UserConnected += OnConnect;
			_server.UserDisconnected += OnDisconnect;
			_server.MessageReceived += OnMessage;
			_server.ConnectedToChat += OnChatOpened;
			_server.AddedChat += OnAddedChat;
			_server.RemovedChat += OnRemovedChat;
			_server.AddedUsersToChat += OnAddedUsersToChat;
			_server.RemovedUsersFromChat += OnRemovedUsersFromChat;

			_data = data;

			_usersProperties = _data.GetUserInfo();
			_infoAllRoom = _data.GetRoomInfo();
			_MessageAtChat = _data.GetAllMessage();
		}

		public async void OnConnect(object sender, UserConnectedEventArgs container)
		{
			if (_usersProperties.TryGetValue(container.ClientName,out UserProperties userProperties))
			{
				if(userProperties.IdConnection == Guid.Empty)
				{
					await Task.Run(() => _server.Send(new List<Guid>() { userProperties.IdConnection }, new ConnectionResponse(ResultRequest.Ok, "Пользователь подключен", userProperties.NumbersRoom).GetContainer()));
					userProperties.IdConnection = container.ClientId;
				}
				else
				{
					await Task.Run(() => _server.Send(new List<Guid>() { userProperties.IdConnection }, new ConnectionResponse(ResultRequest.Failure, "Такой пользователь уже есть", new List<int>()).GetContainer()));
					await Task.Run(() => _server.FreeConnection(userProperties.IdConnection));
				}
			}
			else
			{
				await Task.Run(() => _server.Send(new List<Guid>() { userProperties.IdConnection }, new ConnectionResponse(ResultRequest.Ok, "Новый пользователь зарегистрирован",userProperties.NumbersRoom).GetContainer()));
				_usersProperties.TryAdd(container.ClientName, new UserProperties { IdConnection = container.ClientId, NumbersRoom = new List<int>() });
				
				if(!await Task.Run(() => _data.AddNewUser(new ClientInfo { ClientName = container.ClientName})))
				{
					//Ошибка, не получилось записать
				}
			}
		}
		public async void OnDisconnect(object sender, UserDisconnectedEventArgs container)
		{
			if (_usersProperties.TryGetValue(container.ClientName,out UserProperties userProperties))
			{
				await Task.Run(() => _server.FreeConnection(userProperties.IdConnection));
				_usersProperties.TryUpdate(container.ClientName, new UserProperties{ IdConnection = Guid.Empty, NumbersRoom = userProperties.NumbersRoom }, userProperties);
			}
		}

		public async void OnMessage(object sender, MessageReceivedEventArgs container)
		{
			if(_usersProperties.ContainsKey(container.ClientName)
			   && _infoAllRoom.TryGetValue(container.Room, out InfoRoom infoChat))
			{
				List<Guid> idClientsForSendMessage = new List<Guid>();//Создание списка id для рассылки им сообщений
				foreach (var nameClient in infoChat.NameClients)
				{
					if (_usersProperties.TryGetValue(nameClient, out UserProperties userProperties))
					{
						idClientsForSendMessage.Add(userProperties.IdConnection);
					}
				}
				await Task.Run(() =>  _server.Send(idClientsForSendMessage, new MessageResponse(container.ClientName, container.Message, container.Room).GetContainer()));

				DateTime time = DateTime.Now;
				if (_MessageAtChat.ContainsKey(container.Room))
				{
					_MessageAtChat[container.Room].Add(new MessageInfo { FromMessage = container.ClientName, Text = container.Message, Time = time });
				}

				if (!await Task.Run(() => _data.AddNewMessage(new MessageInfoForDb { NumberRoom = container.Room, 
																					 FromMessage = container.ClientName, 
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
			if(_usersProperties.TryGetValue(container.ClientName,out UserProperties userProperties)
			   && _infoAllRoom.TryGetValue(container.NumberRoom, out InfoRoom infoChat))
			{
				foreach(var nameClientAtChat in infoChat.NameClients)
				{
					if(nameClientAtChat == container.ClientName)
					{
						if(_MessageAtChat.TryGetValue(container.NumberRoom, out List<MessageInfo> messages))
						{
							await Task.Run(() => _server.Send(new List<Guid>() { userProperties.IdConnection }, new ConnectToChatResponse(container.NumberRoom, messages).GetContainer()));
						}

						if (await Task.Run(() => _data.GetMessageFromRoom(container.NumberRoom)))
						{
							//Сигнал серверу на отсутствие доступа к БД
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
			if (_usersProperties.TryGetValue(container.ClientName, out UserProperties userProperties))
			{
				List<Guid> idClientsForSendMessage = new List<Guid>();//Создание списка id для рассылки им сообщений
				foreach (var nameClient in container.Clients)
				{
					if (_usersProperties.TryGetValue(nameClient, out UserProperties clientOfChat))
					{
						idClientsForSendMessage.Add(clientOfChat.IdConnection);
						container.Clients.Remove(nameClient);
					}
				}

				int numberRoom = await Task.Run(() => _data.CreatNewRoom(new CreatingChatInfo { ClientName = container.ClientName, Clients = container.Clients }));
				if(numberRoom != -1)
				{
					await Task.Run(() => _server.Send(idClientsForSendMessage, new AddNewChatResponse(numberRoom, container.Clients).GetContainer()));
					_infoAllRoom.TryAdd(numberRoom, new InfoRoom(container.ClientName, container.Clients));
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
			if (_usersProperties.TryGetValue(container.ClientName, out UserProperties userProperties)
				&& _infoAllRoom.TryGetValue(container.Room,out InfoRoom infoChat))
			{
				if(container.ClientName == infoChat.OwnerChat)
				{
					List<Guid> idClientsForSendMessage = new List<Guid>();//Создание списка id для рассылки им сообщений
					foreach (var nameClient in infoChat.NameClients)
					{
						if (_usersProperties.TryGetValue(nameClient, out UserProperties clientOfChat))
						{
							idClientsForSendMessage.Add(clientOfChat.IdConnection);
						}
					}

					if (!_infoAllRoom.TryRemove(container.Room, out InfoRoom infoRoom))
                    {
						//Не нашел такую комнату
                    }

					await Task.Run(() => _server.Send(idClientsForSendMessage, new RemoveChatResponse(container.Room).GetContainer()));
					if (!await Task.Run(() => _data.RemoveRoom(container.Room)))
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

		public async void OnAddedUsersToChat(object sender, AddedUsersToChatEventArgs container)
		{
			if (_usersProperties.TryGetValue(container.ClientName, out UserProperties userProperties)
				&& _infoAllRoom.TryGetValue(container.Room, out InfoRoom infoChat))
			{
				if(container.ClientName == infoChat.OwnerChat)
				{
					List<Guid> idClientsForSendMessage = new List<Guid>();//Создание списка id для рассылки им сообщений
					foreach (var nameClient in container.Users)
					{
						if (_usersProperties.TryGetValue(nameClient, out UserProperties clientOfChat))
						{
							idClientsForSendMessage.Add(clientOfChat.IdConnection);
							_infoAllRoom[container.Room].NameClients.Add(nameClient);
						}
					}

					await Task.Run(() => _server.Send(idClientsForSendMessage, new AddNewUserToCharResponse(container.ClientName, container.Users, container.Room).GetContainer()));
					if (!await Task.Run(() => _data.AddUserToRoom(new AddClientToChat { Room = container.Room, Users = container.Users })))
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
		public async void OnRemovedUsersFromChat(object sender, RemovedUsersFromChatEventArgs container)
		{
			if (_usersProperties.ContainsKey(container.ClientName)
				&& _infoAllRoom.TryGetValue(container.Room, out InfoRoom infoChat))
			{
				if (container.ClientName == infoChat.OwnerChat)
				{
					List<Guid> idClientsForSendMessage = new List<Guid>();//Создание списка id для рассылки им сообщений
					foreach (var nameClient in infoChat.NameClients)
					{
						if (_usersProperties.TryGetValue(nameClient, out UserProperties clientOfChat))
						{
							idClientsForSendMessage.Add(clientOfChat.IdConnection);
							_infoAllRoom[container.Room].NameClients.Remove(nameClient);
						}
					}

					await Task.Run(() => _server.Send(idClientsForSendMessage, new RemoveUserFromChatResponse(container.ClientName, container.Users, container.Room).GetContainer()));
					
					if (!await Task.Run(() => _data.RemoveUserFromRoom(new RemoveClientFromChat {Room = container.Room, Users = container.Users})))
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
	}
}
