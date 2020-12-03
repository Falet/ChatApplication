using System;
using System.Collections.Generic;
using System.Text;

namespace TestServer.Network
{
    using System.Net;
    public static class TransportFactory
    {
        public static ITransport Create(ConfigServer config)
        {
            switch (config.Protocol)
            {
                case TransportType.WebSocket:
                    return new WsServer(new IPEndPoint(IPAddress.Any, config.Port));
                case TransportType.Tcp:
                    return new TcpServer(new IPEndPoint(IPAddress.Any, config.Port));
                default:
                    throw new ArgumentOutOfRangeException(nameof(config), config, null);
            }
        }
    }
}
