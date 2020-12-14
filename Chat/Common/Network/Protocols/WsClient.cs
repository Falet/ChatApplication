namespace Common.Network
{
    using Packets;
    using Newtonsoft.Json.Linq;
    using WebSocketSharp;
    using System.Net;
    using System;

    public class WsClient
    {
        /*#region Fields

        private readonly IPEndPoint _listenAddress;

        private WebSocket _socket;

        #endregion Fields

        #region Event

        public event EventHandler<ClientConnectedEventArgs> ClientConnected;
        public event EventHandler<MessageReceivedEventArgs> MessageReceived;
        public event EventHandler<ConnectionToChatEventArgs> ConnectedToChat;
        public event EventHandler<AddedChatEventArgs> AddedChat;
        public event EventHandler<RemovedChatEventArgs> RemovedChat;
        public event EventHandler<AddedClientsToChatEventArgs> AddedClientsToChat;
        public event EventHandler<RemovedClientsFromChatEventArgs> RemovedClientsFromChat;

        #endregion Eventent

        public void HandleMessage(MessageContainer container)
        {
            switch (container.Identifier)
            {
                case nameof(ConnectionResponse):
                    {
                        var connectionResponse = ((JObject)container.Payload).ToObject(typeof(ConnectionResponse)) as ConnectionResponse;
                        ClientConnected?.Invoke(this, new ClientConnectedEventArgs());
                        break;
                    }
                case nameof(MessageResponse):
                    {
                        var messageResponse = ((JObject)container.Payload).ToObject(typeof(MessageResponse)) as MessageResponse;
                        MessageReceived?.Invoke(this, new MessageReceivedEventArgs(connection.Login, messageResponse.Message, messageResponse.NumberChat));
                        break;
                    }
                case nameof(ConnectToChatResponse):
                    {
                        var connectionToChat = ((JObject)container.Payload).ToObject(typeof(ConnectToChatResponse)) as ConnectToChatResponse;
                        ConnectedToChat?.Invoke(this, new ConnectionToChatEventArgs(connection.Login, connectionToChat.NumberChat));
                        break;
                    }
                case nameof(AddNewChatResponse):
                    {
                        var addNewChatResponse = ((JObject)container.Payload).ToObject(typeof(AddNewChatResponse)) as AddNewChatResponse;
                        AddedChat?.Invoke(this, new AddedChatEventArgs(connection.Login, addNewChatResponse.Clients));
                        break;
                    }
                case nameof(RemoveChatResponse):
                    {
                        var removeChatResponse = ((JObject)container.Payload).ToObject(typeof(RemoveChatResponse)) as RemoveChatResponse;
                        RemovedChat?.Invoke(this, new RemovedChatEventArgs(connection.Login, removeChatResponse.NumberChat));
                        break;
                    }
                case nameof(AddNewClientToChatResponse):
                    {
                        var addNewClientToChatResponse = ((JObject)container.Payload)
                                                    .ToObject(typeof(AddNewClientToChatResponse)) as AddNewClientToChatResponse;
                        AddedClientsToChat?.Invoke(this, new AddedClientsToChatEventArgs(connection.Login,
                                                                                     addNewClientToChatResponse.NumberChat,
                                                                                     addNewClientToChatResponse.Clients));
                        break;
                    }
                case nameof(RemoveClientFromChatResponse):
                    {
                        var removeClientFromChatResponse = ((JObject)container.Payload)
                                                        .ToObject(typeof(RemoveClientFromChatResponse)) as RemoveClientFromChatResponse;
                        RemovedClientsFromChat?.Invoke(this, new RemovedClientsFromChatEventArgs(connection.Login,
                                                                                             removeClientFromChatResponse.NumberChat,
                                                                                             removeClientFromChatResponse.Clients));
                        break;
                    }
            }
        }*/
    }
}
