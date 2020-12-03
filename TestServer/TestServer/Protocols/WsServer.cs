using System;
using System.Collections.Generic;
using System.Text;

namespace TestServer.Network
{
    using System.Collections.Concurrent;
    using System.Linq;
    using System.Net;

    using Newtonsoft.Json.Linq;

    using WebSocketSharp.Server;
    public class WsServer: ITransport
    {
        #region Fields

        private readonly IPEndPoint _listenAddress;
        private readonly ConcurrentDictionary<Guid, WsConnection> _connections;

        private WebSocketServer _server;

        #endregion Fields

        #region Event

        public event EventHandler<UserConnectedEventArgs> UserConnected;
        public event EventHandler<MessageReceivedEventArgs> MessageReceived;
        public event EventHandler<ConnectionToChatEventArgs> ConnectedToChat;
        public event EventHandler<AddedChatEventArgs> AddedChat;
        public event EventHandler<RemovedChatEventArgs> RemovedChat;
        public event EventHandler<AddedUsersToChatEventArgs> AddedUsersToChat;
        public event EventHandler<RemovedUsersFromChatEventArgs> RemovedUsersFromChat;
        public event EventHandler<UserDisconnectedEventArgs> UserDisconnected;

        #endregion Event

        #region Constructors

        public WsServer(IPEndPoint IPendPoint)
        {
            _server = new WebSocketServer(IPendPoint.Address, IPendPoint.Port, false);
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
                    UserConnected?.Invoke(this, new UserConnectedEventArgs(connection.Login, clientId));
                    break;
                }
                case nameof(DisconnectRequest):
                {
                    var connectionRequest = ((JObject)container.Payload).ToObject(typeof(ConnectionRequest)) as ConnectionRequest;
                    UserDisconnected?.Invoke(this, new UserDisconnectedEventArgs(connection.Login));
                    break;
                }
                case nameof(MessageRequest):
                {
                    var messageRequest = ((JObject)container.Payload).ToObject(typeof(MessageRequest)) as MessageRequest;
                    MessageReceived?.Invoke(this, new MessageReceivedEventArgs(connection.Login, messageRequest.Message, messageRequest.Room));
                    break;
                }
                case nameof(ConnectToChatRequest):
                {
                    var connectionToChat = ((JObject)container.Payload).ToObject(typeof(ConnectToChatRequest)) as ConnectToChatRequest;
                    ConnectedToChat?.Invoke(this, new ConnectionToChatEventArgs(connection.Login, connectionToChat.Room));
                    break;
                }
                case nameof(AddNewChatRequest):
                {
                    var addNewChatRequest = ((JObject)container.Payload).ToObject(typeof(AddNewChatRequest)) as AddNewChatRequest;
                    AddedChat?.Invoke(this, new AddedChatEventArgs(connection.Login, addNewChatRequest.Users));
                    break;
                }
                case nameof(RemoveChatRequest):
                {
                    var removeChatRequest = ((JObject)container.Payload).ToObject(typeof(RemoveChatRequest)) as RemoveChatRequest;
                    RemovedChat?.Invoke(this, new RemovedChatEventArgs(connection.Login, removeChatRequest.Room));
                    break;
                }
                case nameof(AddNewUserToChatRequest):
                {
                    var addNewUserToChatRequest = ((JObject)container.Payload)
                                                .ToObject(typeof(AddNewUserToChatRequest)) as AddNewUserToChatRequest;
                    AddedUsersToChat?.Invoke(this, new AddedUsersToChatEventArgs(connection.Login,
                                                                                 addNewUserToChatRequest.Room,
                                                                                 addNewUserToChatRequest.Users));
                    break;
                }
                case nameof(RemoveUserFromChatRequest):
                {
                    var removeUserFromChatRequest = ((JObject)container.Payload)
                                                    .ToObject(typeof(RemoveUserFromChatRequest)) as RemoveUserFromChatRequest;
                    RemovedUsersFromChat?.Invoke(this, new RemovedUsersFromChatEventArgs(connection.Login,
                                                                                         removeUserFromChatRequest.Room,
                                                                                         removeUserFromChatRequest.Users));
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

        #endregion Methods
    }
}
