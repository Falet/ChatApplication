namespace Server.Network
{
    using Common.Network;
    using Configuration;
    using System.Net;
    using System;
    public class TransportFactory
    {
        #region Methods

        public ITransportServer Create(ConfigServer config)
        {
            switch (config.Protocol)
            {
                case TypeTransport.WebSocket:
                    return new WsServer(new IPEndPoint(IPAddress.Any, config.Port), new HandlerRequestFromClient());
                case TypeTransport.Tcp:
                    return new TcpServer(new IPEndPoint(IPAddress.Any, config.Port), new HandlerRequestFromClient());
                default:
                    throw new ArgumentOutOfRangeException(nameof(config), config, null);
            }
        }

        #endregion Methods
    }
}
