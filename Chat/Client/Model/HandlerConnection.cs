using Common.Network;
using Common.Network.Packets;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Client.Model
{
    class HandlerConnection : IHandlerConnection
    {
        private ITransportClient _transportClient;

        public event EventHandler<ClientDisconnectedEventArgs> ClientDisconnected;
        public event EventHandler<ClientConnectedToServerEventArgs> ClientConnected;
        public event EventHandler<ReceivedInfoAboutAllClientsEventArgs> ReceivedInfoAboutAllClients;
        public event EventHandler<AnotherClientConnectedEventArgs> AnotherClientConnected;

        public HandlerConnection(ITransportClient transportClient, IHandlerResponseFromServer handlerResponseFromServer)
        {
            _transportClient = transportClient;
        }
        public void Connect(string ip, string port, string protocol)
        {
            _transportClient.Connect(ip,int.Parse(port));
        }

        public void Send(string login)
        {
            _transportClient.Send(Container.GetContainer(nameof(ConnectionRequest),new ConnectionRequest(login)));
        }

    }
}
