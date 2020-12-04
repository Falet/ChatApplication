using System;
using System.Collections.Generic;
using System.Text;

namespace TestServer.Network
{
    using System.Threading;
    using System.Threading.Tasks;
    public class HandlerRequestFromServer
    {
        #region Fields

        private Dictionary<string,UserProperties> _usersProperties;//Ключ - имя пользователя
        private Dictionary<int, InfoAllChat> _infoAllChat;//Ключ - номер комнаты
        private Dictionary<int, List<MessageInfo>> _MessageAtChat;//Ключ - номер комнаты
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
            _infoAllChat = _data.GetRoomInfo();
            _MessageAtChat = _data.GetAllMessage();
        }
        public async void OnConnect(object sender, UserConnectedEventArgs container)
        {
            if (_usersProperties.TryGetValue(container.ClientName,out UserProperties userProperties))
            {
                if(userProperties.IdConnection == Guid.Empty)
                {
                    //await Task.Run(() => _server.Send());
                    userProperties.IdConnection = container.ClientId;
                }
                else
                {
                    await Task.Run(() => _server.FreeConnection(userProperties.IdConnection));
                }
            }
            else
            {
                //await Task.Run(() => _server.Send());
                _usersProperties.Add(container.ClientName, new UserProperties { IdConnection = container.ClientId, NumberRoom = new List<int>() });
                
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
                userProperties.IdConnection = Guid.Empty;
            }
        }
        public async void OnMessage(object sender, MessageReceivedEventArgs container)
        {
            if(_usersProperties.ContainsKey(container.ClientName)
               && _infoAllChat.TryGetValue(container.Room, out InfoAllChat infoChat))
            {
                List<Guid> idClentsForSendMessage = new List<Guid>();//Создание списка id для рассылки им сообщений
                foreach (var nameClient in infoChat.NameClients)
                {
                    if (_usersProperties.TryGetValue(nameClient, out UserProperties userProperties))
                    {
                        idClentsForSendMessage.Add(userProperties.IdConnection);

                    }
                }
                MessageContainer messageForUsersAtRoom = new MessageRequest(container.ClientName, container.Message, container.Room).GetContainer();
                _server.Send(idClentsForSendMessage, messageForUsersAtRoom);
                DateTime time = DateTime.Now;
                if (_MessageAtChat.ContainsKey(container.Room))
                {
                    _MessageAtChat[container.Room].Add(new MessageInfo { FromMessage = container.ClientName, Text = container.Message, Time = time });
                }

                if (!await Task.Run(() => _data.AddNewMessage(new MessageInfoForDb { NumberRoom = container.Room, FromMessage = container.ClientName, Text = container.Message, Time = time })))
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
            if(_usersProperties.ContainsKey(container.ClientName)
               && _infoAllChat.TryGetValue(container.NumberRoom, out InfoAllChat infoChat))
            {
                foreach(var nameClientAtChat in infoChat.NameClients)
                {
                    if(nameClientAtChat == container.ClientName)
                    {
                        if(_MessageAtChat.TryGetValue(container.NumberRoom, out List<MessageInfo> messages))
                        {
                            //_server.Send();
                        }
                        else
                        {
                            //Ошибка получения сообщений из списка
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
            if (_usersProperties.ContainsKey(container.ClientName))
            {
                if(await Task.Run(() => _data.CreatNewRoom(new CreatingChatInfo { ClientName = container.ClientName,Clients = container.Clients })))
                {
                    //_server.Send();
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
            if (_usersProperties.ContainsKey(container.ClientName)
                && _infoAllChat.TryGetValue(container.Room,out InfoAllChat infoChat))
            {
                if(container.ClientName == infoChat.OwnerChat)
                {
                    //_server.Send();
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
            if (_usersProperties.ContainsKey(container.ClientName)
                && _infoAllChat.TryGetValue(container.Room, out InfoAllChat infoChat))
            {
                if(container.ClientName == infoChat.OwnerChat)
                {
                    //_server.Send();
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
                && _infoAllChat.TryGetValue(container.Room, out InfoAllChat infoChat))
            {
                if (container.ClientName == infoChat.OwnerChat)
                {
                    //_server.Send();
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
