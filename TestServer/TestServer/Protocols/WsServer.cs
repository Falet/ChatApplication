using System;
using System.Collections.Generic;
using System.Text;

namespace TestServer.Network
{
    using System.Collections.Concurrent;
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

        public event EventHandler<ConnectionStateChangedEventArgs> ConnectionStateChanged;
        public event EventHandler<MessageReceivedEventArgs> MessageReceived;
        public event EventHandler<ConnectionToChatEventArgs> ConnectionToChat;

        #endregion Event

        #region Constructors

        public WsServer()
        {

        }

        #endregion Constructors

        #region Methods

        public void Start(IPEndPoint IPendPoint)
        {
            _server = new WebSocketServer(IPendPoint.Address, IPendPoint.Port, false);
        }

        public void Stop()
        {

        }

        public void AddConnection()
        {

        }

        public void FreeConnection()
        {

        }

        public void Send()
        {

        }

        #endregion Methods
    }
}
