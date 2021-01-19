namespace Common.Network
{
    using System.Collections.Concurrent;
    using System.Linq;
    using System.Net;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
    using Packets;

    using WebSocketSharp.Server;
    public class WsServer: ITransportServer
    {
        #region Fields

        private readonly IPEndPoint _listenAddress;
        private readonly ConcurrentDictionary<Guid, WsConnection> _connections;

        private WebSocketServer _server;

        #endregion Fields

        #region Event

        public event EventHandler<ClientConnectedEventArgs> ClientConnected;
        public event EventHandler<MessageReceivedEventArgs> MessageReceived;
        public event EventHandler<ConnectionToChatEventArgs> ConnectedToChat;
        public event EventHandler<AddedNewChatEventArgs> AddedChat;
        public event EventHandler<RemovedChatEventArgs> RemovedChat;
        public event EventHandler<AddedClientsToChatEventArgs> AddedClientsToChat;
        public event EventHandler<RemovedClientsFromChatEventArgs> RemovedClientsFromChat;
        public event EventHandler<ClientDisconnectedEventArgs> ClientDisconnected;
        public event EventHandler<ClientRequestedNumbersChatEventArgs> RequestNumbersChats;
        public event EventHandler<InfoAboutAllClientsEventArgs> RequestInfoAllClient;

        #endregion Event

        #region Constructors

        public WsServer(IPEndPoint IPendPoint)
        {
            _listenAddress = IPendPoint;
            _connections = new ConcurrentDictionary<Guid, WsConnection>();
            _server = new WebSocketServer(_listenAddress.Address, _listenAddress.Port, false);
            _server.AddWebSocketService<WsConnection>("/",
                client =>
                {
                    client.AddServer(this);
                });
            _server.Start();
            Console.WriteLine("Start");
        }

        #endregion Constructors

        #region Methods

        public void AddConnection(WsConnection connection)
        {
            _connections.TryAdd(connection.Id, connection);
        }

        public void HandleMessage(Guid clientId, MessageContainer container)
        {
            if (!_connections.TryGetValue(clientId, out WsConnection connection))
                return;

            switch (container.Identifier)
            {
                case nameof(ConnectionRequest):
                {
                    var connectionRequest = ((JObject)container.Payload).ToObject(typeof(ConnectionRequest)) as ConnectionRequest;
                    ClientConnected?.Invoke(this, new ClientConnectedEventArgs(connectionRequest.ClientName, clientId));
                    break;
                }
                case nameof(InfoAboutAllClientsRequest):
                {
                    var infoClientRequest = ((JObject)container.Payload).ToObject(typeof(InfoAboutAllClientsRequest)) as InfoAboutAllClientsRequest;
                    RequestInfoAllClient?.Invoke(this, new InfoAboutAllClientsEventArgs(infoClientRequest.NameClient));
                    break;
                }
                case nameof(MessageRequest):
                {
                    var messageRequest = ((JObject)container.Payload).ToObject(typeof(MessageRequest)) as MessageRequest;
                    MessageReceived?.Invoke(this, new MessageReceivedEventArgs(messageRequest.ClientName, messageRequest.Message, messageRequest.NumberChat));
                    break;
                }
                case nameof(ConnectToChatRequest):
                {
                    var connectionToChat = ((JObject)container.Payload).ToObject(typeof(ConnectToChatRequest)) as ConnectToChatRequest;
                    ConnectedToChat?.Invoke(this, new ConnectionToChatEventArgs(connectionToChat.ClientName, connectionToChat.NumberChat));
                    break;
                }
                case nameof(AddNewChatRequest):
                {
                    var addNewChatRequest = ((JObject)container.Payload).ToObject(typeof(AddNewChatRequest)) as AddNewChatRequest;
                    AddedChat?.Invoke(this, new AddedNewChatEventArgs(addNewChatRequest.NameClientSender, addNewChatRequest.Clients));
                    break;
                }
                case nameof(RemoveChatRequest):
                {
                    var removeChatRequest = ((JObject)container.Payload).ToObject(typeof(RemoveChatRequest)) as RemoveChatRequest;
                    RemovedChat?.Invoke(this, new RemovedChatEventArgs(removeChatRequest.NameOfRemover, removeChatRequest.NumberChat));
                    break;
                }
                case nameof(AddNewClientToChatRequest):
                {
                    var addNewClientToChatRequest = ((JObject)container.Payload)
                                                .ToObject(typeof(AddNewClientToChatRequest)) as AddNewClientToChatRequest;
                    AddedClientsToChat?.Invoke(this, new AddedClientsToChatEventArgs(addNewClientToChatRequest.ClientName,
                                                                                 addNewClientToChatRequest.NumberChat,
                                                                                 addNewClientToChatRequest.Clients));
                    break;
                }
                case nameof(RemoveClientFromChatRequest):
                {
                    var removeClientFromChatRequest = ((JObject)container.Payload)
                                                    .ToObject(typeof(RemoveClientFromChatRequest)) as RemoveClientFromChatRequest;
                    RemovedClientsFromChat?.Invoke(this, new RemovedClientsFromChatEventArgs(removeClientFromChatRequest.ClientName,
                                                                                         removeClientFromChatRequest.NumberChat,
                                                                                         removeClientFromChatRequest.Clients));
                    break;
                }
                case nameof(GetNumbersAccessibleChatsRequest):
                {
                    var requestNumbersChats = ((JObject)container.Payload)
                                                    .ToObject(typeof(GetNumbersAccessibleChatsRequest)) as GetNumbersAccessibleChatsRequest;
                    RequestNumbersChats?.Invoke(this, new ClientRequestedNumbersChatEventArgs(requestNumbersChats.NameClient));
                    break;
                }
            }
        }

        public void FreeConnection(Guid ClientId)
        {
            _connections.TryRemove(ClientId, out WsConnection connection);
            if(connection.Login != null)
            {
                ClientDisconnected.Invoke(this, new ClientDisconnectedEventArgs(ClientId, connection.Login));
            }
        }

        public void Send(List<Guid> ListClientId, MessageContainer message)
        {
            foreach(var id in ListClientId)
            {
                if (!_connections.TryGetValue(id, out WsConnection connection))
                    continue;
                connection.Send(message);
            }
        }
        public void SendAll(Guid clientGuid, MessageContainer message)
        {
            foreach (var connection in _connections)
            {
                if(connection.Key != clientGuid)
                {
                    connection.Value.Send(message);
                }
            }
        }
        public void SetLoginConnection(Guid clientGuid, string nameClient)
        {
            if(_connections.TryGetValue(clientGuid, out WsConnection wsConnection))
            {
                wsConnection.Login = nameClient;
            }
        }
        #endregion Methods
    }
}
