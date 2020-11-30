using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using WebSocketSharp.Server;

namespace TestServer.Network
{
    class Connection : WebSocketBehavior, IConnection
    {
        private WebSocketServer _server;

        #region Methods

        public Connection()
        {

        }

        public void Send()
        {

        }

        public void Start()
        {

            server = new WebSocketServer(IPAddress.Any, 65000, false);
            //server.AddWebSocketService<ChatG>("/", client => { client.AddServer(this); });
            server.Start();
        }

        #endregion Methods
    }
}
