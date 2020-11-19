using System;
using System.Collections.Generic;
using System.Text;

namespace TestServer
{
    class Connection
    {
        WebSocketServer server = new WebSocketServer(IPAddress.Any, 65000, false);
        server.AddWebSocketService<WsConnection>("/",
                client =>
                {
                    client.AddServer(this);
                });
    }
}
