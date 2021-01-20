namespace Common.Network
{
    using System.Collections.Concurrent;
    using System.Net;
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

        #region Constructors

        public WsServer(IPEndPoint IPendPoint, IHandlerRequestFromClient handlerRequestFromClient)
        {
            _listenAddress = IPendPoint;
            _connections = new ConcurrentDictionary<Guid, WsConnection>();
            _server = new WebSocketServer(_listenAddress.Address, _listenAddress.Port, false);
            _server.AddWebSocketService<WsConnection>("/",
                client =>
                {
                    client.AddServer(this);
                    client.AddParserPacket(handlerRequestFromClient);
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
