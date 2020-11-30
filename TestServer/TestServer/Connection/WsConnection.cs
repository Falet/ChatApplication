using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace TestServer.Network
{
    using WebSocketSharp;
    using WebSocketSharp.Server;
    class WsConnection : WebSocketBehavior
    {
        private WsServer _server;

        //private readonly ConcurrentQueue<MessageContainer> _sendQueue;

        public WsConnection()
        {

        }

        #region Methods

        public void AddServer(WsServer server)
        {
            _server = server;
        }
        protected override void OnOpen()
        {
        }

        protected override void OnClose(CloseEventArgs e)
        {
        }

        protected override void OnMessage(MessageEventArgs e)
        {

        }
        #endregion Methods
    }
}
