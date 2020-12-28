using Common.Network;
using System;
using System.Collections.Generic;
using System.Text;

namespace Client.Model
{
    class HandlerConnection : IHandlerConnection
    {
        public event EventHandler<ClientDisconnectedEventArgs> ClientDisconnected;
        public event EventHandler<ClientConnectedToServerEventArgs> ClientConnected;
        public event EventHandler<ReceivedInfoAboutAllClientsEventArgs> ReceivedInfoAboutAllClients;

        public void Connect(string ip, string port, string protocol)
        {
            throw new NotImplementedException();
        }

        public void Send(string login)
        {
            throw new NotImplementedException();
        }
    }
}
