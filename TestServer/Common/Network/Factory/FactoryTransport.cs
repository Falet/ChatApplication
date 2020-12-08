namespace Common.Network
{
    using System.Net;
    using System;
    public static class TransportFactory
    {
        #region Methods

        public static ITransportServer Create(ConfigServer config)
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

        #endregion Methods
    }
}
