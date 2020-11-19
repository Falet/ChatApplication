using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using WebSocketSharp.Server;

namespace TestServer
{
    class Connection
    {
        WebSocketServer server = new WebSocketServer(IPAddress.Any, 65000, false);
        server.AddWebSocketService<ChatG>("/",client =>{client.AddServer(this);});
    }
}
