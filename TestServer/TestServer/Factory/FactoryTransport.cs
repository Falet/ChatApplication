using System;
using System.Collections.Generic;
using System.Text;

namespace TestServer.Network
{
    public static class TransportFactory
    {
        public static ITransport Create(TransportType type)
        {
            switch (type)
            {
                case TransportType.WebSocket:
                    return new WsServer();
                case TransportType.Tcp:
                    return new TcpServer();
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}
