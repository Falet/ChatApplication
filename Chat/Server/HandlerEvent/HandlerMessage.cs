namespace Server.Network
{
    using Common.Network;
    using Common.Network.Packets;
    using Server.DataBase;
    using Server.Network;
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class HandlerMessage
    {
        public ConcurrentDictionary<int, List<MessageInfo>> MessagesAtChat { get; }//Ключ - номер комнаты

        #region Fields

        private IHandlerRequestToData _data;
        private ITransportServer _server;
        private HandlerConnection _connection;
        private HandlerChat _chats;

        #endregion Fields
        public HandlerMessage(ITransportServer server,IHandlerRequestFromClient handlerRequestFromClient, IHandlerRequestToData data, HandlerConnection connection, HandlerChat chats)
        {
            _server = server;
            handlerRequestFromClient.MessageReceived += OnMessage;
            handlerRequestFromClient.ConnectedToChat += OnChatOpened;

            _data = data;
            MessagesAtChat = _data.GetAllMessageFromChats();

            _connection = connection;
            _chats = chats;
        }
        public void OnChatOpened(object sender, ConnectionToChatEventArgs container)
        {
            if (_connection.cachedClientName.TryGetValue(container.NameOfClient, out Guid clientGuid)
               && _chats.InfoChats.TryGetValue(container.NumberChat, out InfoChat infoChat))
            {
                foreach (var nameClientAtChat in infoChat.NameOfClients)
                {
                    if (nameClientAtChat == container.NameOfClient)
                    {
                        if (MessagesAtChat.TryGetValue(container.NumberChat, out List<MessageInfo> messages) && clientGuid != Guid.Empty)
                        {
                            var SendMessageToServer = Task.Run
                            (
                            () => _server.Send(new List<Guid> { clientGuid },
                                         Container.GetContainer(nameof(ConnectToChatResponse),
                                         new ConnectToChatResponse(container.NumberChat, messages)))
                            );
                        }
                    }
                }
            }
        }

        public async void OnMessage(object sender, MessageReceivedEventArgs container)
        {
            if (_connection.cachedClientName.ContainsKey(container.NameOfClient)
               && _chats.InfoChats.TryGetValue(container.NumberChat, out InfoChat infoChat))
            {
                List<Guid> idClientsForSendMessage = new List<Guid>();//Создание списка id для рассылки им сообщений
                foreach (var nameClient in infoChat.NameOfClients)
                {
                    if (_connection.cachedClientName.TryGetValue(nameClient, out Guid clientGuid) && clientGuid != Guid.Empty)
                    {
                        idClientsForSendMessage.Add(clientGuid);
                    }
                }
                var SendMessageToServer = Task.Run(() => _server.Send(idClientsForSendMessage,
                                                                      Container.GetContainer(nameof(MessageResponse),
                                                                      new MessageResponse(new MessageInfo(container.NameOfClient, container.Message, DateTime.Now), container.NumberChat)))
                                                    );

                DateTime time = DateTime.Now;
                if (MessagesAtChat.TryGetValue(container.NumberChat, out List<MessageInfo> allMessageAtChat))
                {
                    var lastValueMessages = allMessageAtChat;
                    allMessageAtChat.Add(new MessageInfo(container.NameOfClient, container.Message, time));
                    MessagesAtChat.TryUpdate(container.NumberChat, allMessageAtChat, lastValueMessages);
                }

                if (!await Task.Run(() => _data.AddNewMessage(new MessageInfoForDataBase
                                                                {
                                                                    NumberChat = container.NumberChat,
                                                                    FromMessage = container.NameOfClient,
                                                                    Text = container.Message,
                                                                    Time = time
                                                                })))
                {
                    //Сообщение не удалось добавить, сигнал серверу на запрет приема сообщений до добавления сообщения
                }
            }
        }
    }
}
