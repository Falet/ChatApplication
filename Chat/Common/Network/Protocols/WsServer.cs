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
        public event EventHandler<AddedChatEventArgs> AddedChat;
        public event EventHandler<RemovedChatEventArgs> RemovedChat;
        public event EventHandler<AddedClientsToChatEventArgs> AddedClientsToChat;
        public event EventHandler<RemovedClientsFromChatEventArgs> RemovedClientsFromChat;
        public event EventHandler<ClientDisconnectedEventArgs> ClientDisconnected;

        #endregion Event

        #region Constructors

        public WsServer(IPEndPoint IPendPoint)
        {
            _listenAddress = IPendPoint;
            _server = new WebSocketServer(_listenAddress.Address, _listenAddress.Port, false);
            _connections = new ConcurrentDictionary<Guid, WsConnection>();
        }

        #endregion Constructors

        #region Methods

        public void Start()
        {
            _server = new WebSocketServer(_listenAddress.Address, _listenAddress.Port, false);
            _server.AddWebSocketService<WsConnection>("/",
                client =>
                {
                    client.AddServer(this);
                });
            _server.Start();
        }

        public void Stop()
        {
            _server?.Stop();
            _server = null;

            var connections = _connections.Select(item => item.Value).ToArray();
            foreach (var connection in connections)
            {
                connection.Close();
            }

            _connections.Clear();
        }

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
                    ClientConnected?.Invoke(this, new ClientConnectedEventArgs(connection.Login, clientId));
                    break;
                }
                case nameof(DisconnectRequest):
                {
                    var connectionRequest = ((JObject)container.Payload).ToObject(typeof(ConnectionRequest)) as ConnectionRequest;
                    ClientDisconnected?.Invoke(this, new ClientDisconnectedEventArgs(connection.Login));
                    break;
                }
                case nameof(MessageRequest):
                {
                    var messageRequest = ((JObject)container.Payload).ToObject(typeof(MessageRequest)) as MessageRequest;
                    MessageReceived?.Invoke(this, new MessageReceivedEventArgs(connection.Login, messageRequest.Message, messageRequest.NumberChat));
                    break;
                }
                case nameof(ConnectToChatRequest):
                {
                    var connectionToChat = ((JObject)container.Payload).ToObject(typeof(ConnectToChatRequest)) as ConnectToChatRequest;
                    ConnectedToChat?.Invoke(this, new ConnectionToChatEventArgs(connection.Login, connectionToChat.NumberChat));
                    break;
                }
                case nameof(AddNewChatRequest):
                {
                    var addNewChatRequest = ((JObject)container.Payload).ToObject(typeof(AddNewChatRequest)) as AddNewChatRequest;
                    AddedChat?.Invoke(this, new AddedChatEventArgs(connection.Login, addNewChatRequest.Clients));
                    break;
                }
                case nameof(RemoveChatRequest):
                {
                    var removeChatRequest = ((JObject)container.Payload).ToObject(typeof(RemoveChatRequest)) as RemoveChatRequest;
                    RemovedChat?.Invoke(this, new RemovedChatEventArgs(connection.Login, removeChatRequest.NumberChat));
                    break;
                }
                case nameof(AddNewClientToChatRequest):
                {
                    var addNewClientToChatRequest = ((JObject)container.Payload)
                                                .ToObject(typeof(AddNewClientToChatRequest)) as AddNewClientToChatRequest;
                    AddedClientsToChat?.Invoke(this, new AddedClientsToChatEventArgs(connection.Login,
                                                                                 addNewClientToChatRequest.NumberChat,
                                                                                 addNewClientToChatRequest.Clients));
                    break;
                }
                case nameof(RemoveClientFromChatRequest):
                {
                    var removeClientFromChatRequest = ((JObject)container.Payload)
                                                    .ToObject(typeof(RemoveClientFromChatRequest)) as RemoveClientFromChatRequest;
                    RemovedClientsFromChat?.Invoke(this, new RemovedClientsFromChatEventArgs(connection.Login,
                                                                                         removeClientFromChatRequest.NumberChat,
                                                                                         removeClientFromChatRequest.Clients));
                    break;
                }
            }
        }

        public void FreeConnection(Guid ClientId)
        {
            _connections.TryRemove(ClientId, out WsConnection connection);
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
        public void SendAll(MessageContainer message)
        {
            foreach (var connection in _connections)
            {
                connection.Value.Send(message);
            }
        }
        #endregion Methods
    }
}
