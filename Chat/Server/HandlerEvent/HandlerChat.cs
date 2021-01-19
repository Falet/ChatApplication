namespace Server.Network
{
    using Common.Network;
    using Common.Network.Packets;
    using Server.DataBase;
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class HandlerChat
    {
        #region Const

        const int NumberGeneralChat = 1;

        #endregion Const

        #region Fields

        private ConcurrentDictionary<string, ClientProperties> _cachedClientProperies;//Ключ - имя пользователя
        private IHandlerRequestToData _data;
        private ITransportServer _server;
        private HandlerConnection _connection;

        #endregion Fields

        #region Properties

        public ConcurrentDictionary<int, InfoChat> InfoChats { get; }//Ключ - номер комнаты

        #endregion Properties

        #region Constructors

        public HandlerChat(ITransportServer server, IHandlerRequestFromClient handlerRequestFromClient, IHandlerRequestToData data, HandlerConnection connection)
        {
            _server = server;

            handlerRequestFromClient.AddedChat += OnAddedChat;
            handlerRequestFromClient.RemovedChat += OnRemovedChat;
            handlerRequestFromClient.AddedClientsToChat += OnAddedClientsToChat;
            handlerRequestFromClient.RemovedClientsFromChat += OnRemovedClientsFromChat;
            handlerRequestFromClient.RequestNumbersChats += OnRequestNumbersChats;

            _data = data;
            _cachedClientProperies = _data.GetInfoAboutLinkClientToChat();
            InfoChats = _data.GetInfoAboutAllChat();
            _connection = connection;
        }

        #endregion Constructors


        #region Methods
        public async void OnAddedChat(object sender, AddedNewChatEventArgs container)
        {
            if (_connection.cachedClientName.TryGetValue(container.NameOfClientSender, out Guid clientCreatorGuid))
            {
                int numberChat = await Task.Run(() => _data.CreatNewChat(new CreatingChatInfo { NameOfClientSender = container.NameOfClientSender, NameOfClients = container.NameOfClientsForAdd }));
                if (numberChat != -1)
                {
                    List<Guid> idClientsForSendMessage = new List<Guid>();//Создание списка id для рассылки им сообщений
                    List<string> NameForChange = container.NameOfClientsForAdd;
                    await Task.Run(() => CreateUserListForChangeInfoChat(ref NameForChange, numberChat, ref idClientsForSendMessage));

                    var SendMessageToServer = Task.Run(() => _server.Send(idClientsForSendMessage, Container.GetContainer(nameof(AddNewChatResponse),new AddNewChatResponse(container.NameOfClientSender,numberChat, container.NameOfClientsForAdd))));
                    if(_cachedClientProperies.TryGetValue(container.NameOfClientSender, out ClientProperties value))
                    {
                        var bufferForAddChat = value;
                        value.NumbersChat.Add(numberChat);
                        _cachedClientProperies.TryUpdate(container.NameOfClientSender, value, bufferForAddChat);
                    }
                    InfoChats.TryAdd(numberChat, new InfoChat { OwnerChat = container.NameOfClientSender, NameOfClients = container.NameOfClientsForAdd });
                    if(!await _data.AddClientToChat(new AddClientToChat { NumberChat = numberChat, NameOfClients = NameForChange }))
                    {
                        //Ошибка добавления клиентов
                    }
                }
                else
                {
                    //Ошибка добавления чата
                }
            }
        }
        public async void OnRemovedChat(object sender, RemovedChatEventArgs container)
        {
            if (_cachedClientProperies.TryGetValue(container.NameClient, out ClientProperties clientProperties)
                && InfoChats.TryGetValue(container.NumberChat, out InfoChat infoChat))
            {
                if (container.NameClient == infoChat.OwnerChat)
                {
                    List<Guid> idClientsForSendMessage = new List<Guid>();//Создание списка id для рассылки им сообщений
                    List<string> NameForChange = infoChat.NameOfClients;
                    await Task.Run(() => CreateUserListForChangeInfoChat(ref NameForChange, container.NumberChat, ref idClientsForSendMessage));

                    var SendMessageToServer = Task.Run(() => _server.Send(idClientsForSendMessage,Container.GetContainer(nameof(RemoveChatResponse), new RemoveChatResponse(container.NameClient, container.NumberChat))));

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
        }

        public async void OnAddedClientsToChat(object sender, AddedClientsToChatEventArgs container)
        {
            if (_cachedClientProperies.TryGetValue(container.NameOfClientSender, out ClientProperties clientProperties)
                && InfoChats.TryGetValue(container.NumberChat, out InfoChat infoChat))
            {
                if (container.NameOfClientSender == infoChat.OwnerChat)
                {
                    List<Guid> idClientsForSendMessage = new List<Guid>();//Создание списка id для рассылки им сообщений
                    List<string> NameForChange = container.Clients;
                    await Task.Run(() => CreateUserListForChangeInfoChat(ref NameForChange, container.NumberChat, ref idClientsForSendMessage));
                    var SendMessageForCreateChat = Task.Run
                    (
                    () => _server.Send(idClientsForSendMessage, 
                                        Container.GetContainer(nameof(AddNewChatResponse), 
                                        new AddNewChatResponse(container.NameOfClientSender, container.NumberChat, infoChat.NameOfClients)))
                    );

                    InfoChat buffer = infoChat;
                    foreach(var item in NameForChange)
                    {
                        infoChat.NameOfClients.Add(item);
                        if(_cachedClientProperies.TryGetValue(item, out ClientProperties value))
                        {
                            var bufferClientProp = value;
                            value.NumbersChat.Add(container.NumberChat);
                            _cachedClientProperies.TryUpdate(item, value, bufferClientProp);
                        }
                    }
                    List<Guid> idClientsForSendMessageAddClient = new List<Guid>();
                    await Task.Run(() => AddNamesForMail(infoChat.NameOfClients, ref idClientsForSendMessageAddClient));

                    var SendMessageToServer = Task.Run(() => _server.Send(idClientsForSendMessageAddClient, Container.GetContainer(nameof(AddNewClientToChatResponse),new AddNewClientToChatResponse(container.NameOfClientSender, NameForChange, container.NumberChat))));

                    if(InfoChats.TryUpdate(container.NumberChat, infoChat, buffer))
                    {
                        Console.WriteLine("Обновил");
                    }

                    if (!await Task.Run(() => _data.AddClientToChat(new AddClientToChat { NumberChat = container.NumberChat, NameOfClients = NameForChange })))
                    {
                        //Ошибка на добавление в БД
                    }
                }
            }
        }
        public async void OnRemovedClientsFromChat(object sender, RemovedClientsFromChatEventArgs container)
        {
            if (_cachedClientProperies.TryGetValue(container.NameOfRemover, out ClientProperties clientProperties)
                && InfoChats.TryGetValue(container.NumberChat, out InfoChat infoChat))
            {
                if (container.NameOfRemover == infoChat.OwnerChat)
                {
                    List<Guid> idClientsForRemoveChat = new List<Guid>();//Создание списка id для рассылки им сообщений
                    List<string> NamesForChange = container.Clients;

                    await Task.Run(() => CreateUserListForChangeInfoChat(ref NamesForChange, container.NumberChat, ref idClientsForRemoveChat));
                    var SendMessageForRemoveChat = Task.Run(() => _server.Send(idClientsForRemoveChat, Container.GetContainer(nameof(RemoveChatResponse), 
                                                                               new RemoveChatResponse(container.NameOfRemover, container.NumberChat))));

                    InfoChat bufferForUpdate = infoChat;
                    foreach (var item in container.Clients)
                    {
                        infoChat.NameOfClients.Remove(item);
                    }

                    List<Guid> idClientsForRemovedClient = new List<Guid>();
                    await Task.Run(() => AddNamesForMail(infoChat.NameOfClients, ref idClientsForRemovedClient));
                    var SendMessageChangeClientList = Task.Run(() => 
                    _server.Send(idClientsForRemovedClient, Container.GetContainer(nameof(RemoveClientFromChatResponse),
                                                         new RemoveClientFromChatResponse(container.NameOfRemover, container.Clients, container.NumberChat)))
                    );

                    InfoChats.TryUpdate(container.NumberChat, infoChat, bufferForUpdate);

                    if (!await Task.Run(() => _data.RemoveClientFromChat(new RemoveClientFromChat { NumberChat = container.NumberChat, NameOfClients = container.Clients })))
                    {
                        //Ошибка на удаление в БД
                    }
                }
            }
        }
        public async void OnRequestNumbersChats(object sender, ClientRequestedNumbersChatEventArgs container)
        {
            if(_connection.cachedClientName.TryGetValue(container.NameOfClientSender,out Guid clientGuid))
            {
                List<InfoAboutChat> AllInfoAboutChat = new List<InfoAboutChat>();

                if (_cachedClientProperies.TryGetValue(container.NameOfClientSender, out ClientProperties clientProperties))
                {
                    foreach (var numberChat in clientProperties.NumbersChat)
                    {
                        if (InfoChats.TryGetValue(numberChat, out InfoChat infoChat))
                        {
                            AllInfoAboutChat.Add(new InfoAboutChat(numberChat, infoChat.OwnerChat, infoChat.NameOfClients));
                        }
                    }
                }
                else
                {
                    _cachedClientProperies.TryAdd(container.NameOfClientSender,
                                                    new ClientProperties() { NumbersChat = new List<int>() { NumberGeneralChat } });
                    if (InfoChats.TryGetValue(NumberGeneralChat, out InfoChat infoChat))
                    {
                        InfoChat lastValue = infoChat;
                        infoChat.NameOfClients.Add(container.NameOfClientSender);
                        InfoChats.TryUpdate(NumberGeneralChat, infoChat, lastValue);
                    }
                    AllInfoAboutChat.Add(new InfoAboutChat(NumberGeneralChat, infoChat.OwnerChat, infoChat.NameOfClients));

                    var SendMessageAboutConnectNewClient = Task.Run
                    (
                    () => _server.SendAll(clientGuid,
                                 Container.GetContainer(nameof(AddNewClientToChatResponse),
                                 new AddNewClientToChatResponse("Server", new List<string> { container.NameOfClientSender }, NumberGeneralChat)))
                    );
                    if (!await Task.Run(() => _data.AddClientToChat(new AddClientToChat { NumberChat = NumberGeneralChat, 
                                                                      NameOfClients = new List<string> { container.NameOfClientSender } })))
                    {

                    }
                }
                var SendMessageToServer = Task.Run
                    (
                    () => _server.Send(new List<Guid> { clientGuid },
                                 Container.GetContainer(nameof(NumbersAccessibleChatsResponse),
                                 new NumbersAccessibleChatsResponse(AllInfoAboutChat)))
                    );
            }
        }

        private void CreateUserListForChangeInfoChat(ref List<string> namesClientForCreat,int numberChat,ref List<Guid> namesForMail)
        {
            foreach (var nameClient in namesClientForCreat)
            {
                if (_cachedClientProperies.TryGetValue(nameClient, out ClientProperties clientOfChat))
                {
                    if (_connection.cachedClientName.TryGetValue(nameClient, out Guid clientGuid) && clientGuid != Guid.Empty)
                    {
                        namesForMail.Add(clientGuid);
                    }
                    var lastValueChatsClient = clientOfChat;
                    clientOfChat.NumbersChat.Remove(numberChat);
                    _cachedClientProperies.TryUpdate(nameClient, clientOfChat, lastValueChatsClient);
                }
                else
                {
                    namesClientForCreat.Remove(nameClient);
                }
            }
        }

        private void AddNamesForMail(List<string> namesClientForCreat, ref List<Guid> namesForMail)
        {
            foreach (var nameClientAtChat in namesClientForCreat)
            {
                if (_connection.cachedClientName.TryGetValue(nameClientAtChat, out Guid clientGuid) && clientGuid != Guid.Empty)
                {
                    namesForMail.Add(clientGuid);
                }
            }
        }
        #endregion Methods
    }
}
