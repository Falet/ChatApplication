using Common.Network;
using Common.Network.Packets;
using Server.DataBase;
using Server.Network;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Network
{
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
         
        public ConcurrentDictionary<int, InfoChat> InfoChats { get; }//Ключ - номер комнаты

        public HandlerChat(ITransportServer server, IHandlerRequestToData data, HandlerConnection connection)
        {
            _server = server;

            _server.AddedChat += OnAddedChat;
            _server.RemovedChat += OnRemovedChat;
            _server.AddedClientsToChat += OnAddedClientsToChat;
            _server.RemovedClientsFromChat += OnRemovedClientsFromChat;
            _server.RequestNumbersChats += OnRequestNumbersChats;

            _data = data;
            _cachedClientProperies = _data.GetInfoAboutLinkClientToChat();
            InfoChats = _data.GetInfoAboutAllChat();
            _connection = connection;

        }

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

                    if (clientCreatorGuid != Guid.Empty)
                    {
                        idClientsForSendMessage.Add(clientCreatorGuid);
                    }

                    var SendMessageToServer = Task.Run(() => _server.Send(idClientsForSendMessage, Container.GetContainer(nameof(AddNewChatResponse),new AddNewChatResponse(container.NameOfClientSender,numberChat, container.NameOfClientsForAdd))));
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
                    List<string> NameForChange = infoChat.NameOfClients;
                    await Task.Run(() => CreateUserListForChangeInfoChat(ref NameForChange, container.NumberChat, ref idClientsForSendMessage));

                    var SendMessageToServer = Task.Run(() => _server.Send(idClientsForSendMessage,Container.GetContainer(nameof(RemoveChatResponse), new RemoveChatResponse(container.NumberChat))));

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

                    InfoChat buffer = infoChat;
                    infoChat.NameOfClients.Union(NameForChange);
                    await Task.Run(() => AddNamesForMail(infoChat.NameOfClients, ref idClientsForSendMessage));

                    var SendMessageToServer = Task.Run(() => _server.Send(idClientsForSendMessage, Container.GetContainer(nameof(AddNewClientToChatResponse),new AddNewClientToChatResponse(container.NameOfClientSender, NameForChange, container.NumberChat))));

                    InfoChats.TryUpdate(container.NumberChat, infoChat, buffer);

                    if (!await Task.Run(() => _data.AddClientToChat(new AddClientToChat { NumberChat = container.NumberChat, NameOfClients = NameForChange })))
                    {
                        //Ошибка на добавление в БД
                    }
                }
            }
            else
            {
                //Ошибка
            }
        }
        public async void OnRemovedClientsFromChat(object sender, RemovedClientsFromChatEventArgs container)
        {
            if (_cachedClientProperies.TryGetValue(container.NameOfRemover, out ClientProperties clientProperties)
                && InfoChats.TryGetValue(container.NumberChat, out InfoChat infoChat))
            {
                if (container.NameOfRemover == infoChat.OwnerChat)
                {
                    List<Guid> idClientsForSendMessage = new List<Guid>();//Создание списка id для рассылки им сообщений
                    List<string> NameForChange = container.Clients;
                    await Task.Run(() => CreateUserListForChangeInfoChat(ref NameForChange, container.NumberChat, ref idClientsForSendMessage));

                    InfoChat buffer = infoChat;
                    infoChat.NameOfClients.Except(container.Clients);
                    await Task.Run(() => AddNamesForMail(infoChat.NameOfClients, ref idClientsForSendMessage));

                    var SendMessageToServer = Task.Run(() => _server.Send(idClientsForSendMessage, Container.GetContainer(nameof(RemoveClientFromChatResponse),new RemoveClientFromChatResponse(container.NameOfRemover, container.Clients, container.NumberChat))));

                    InfoChats.TryUpdate(container.NumberChat, infoChat, buffer);

                    if (!await Task.Run(() => _data.RemoveClientFromChat(new RemoveClientFromChat { NumberChat = container.NumberChat, NameOfClients = container.Clients })))
                    {
                        //Ошибка на удаление в БД
                    }
                }
            }
            else
            {
                //Ошибка
            }
        }
        public void OnRequestNumbersChats(object sender, ClientRequestedNumbersChatEventArgs container)
        {
            if(_connection.cachedClientName.TryGetValue(container.NameOfClientSender,out Guid clientGuid))
            {
                Dictionary<LinkNumberChatCreator, ClientsAtChat> AllInfoAboutChat = new Dictionary<LinkNumberChatCreator, ClientsAtChat>();

                if (_cachedClientProperies.TryGetValue(container.NameOfClientSender, out ClientProperties clientProperties))
                {
                    foreach(var numberChat in clientProperties.NumbersChat)
                    {
                        if (InfoChats.TryGetValue(numberChat, out InfoChat infoChat))
                        {
                            AllInfoAboutChat.Add(new LinkNumberChatCreator(numberChat, infoChat.OwnerChat), new ClientsAtChat { NamesOfClients = infoChat.NameOfClients });
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
                    if (InfoChats.TryGetValue(NumberGeneralChat, out InfoChat infoChat1))
                    {
                        AllInfoAboutChat.Add(new LinkNumberChatCreator(NumberGeneralChat, infoChat1.OwnerChat), new ClientsAtChat { NamesOfClients = infoChat1.NameOfClients });
                    }
                }
                var SendMessageToServer = Task.Run
                    (
                    () => _server.Send(new List<Guid> { clientGuid },
                                 Container.GetContainer(nameof(GetNumbersAccessibleChatsResponse),
                                 new GetNumbersAccessibleChatsResponse(AllInfoAboutChat)))
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
