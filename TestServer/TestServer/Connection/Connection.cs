using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using WebSocketSharp.Server;

namespace TestServer.Network
{
    class Connection
    {
        WebSocketServer server;
        public Connection()
        {

        }
        public void Start()
        {

            server = new WebSocketServer(IPAddress.Any, 65000, false);
            //server.AddWebSocketService<ChatG>("/", client => { client.AddServer(this); });
            server.Start();
        }
    }
}
