using Common.Network;
using System;
using System.Collections.Generic;
using System.Text;

namespace Client.Model
{
    public interface IHandlerConnection
    {
        public event EventHandler<ClientConnectedToServerEventArgs> ClientConnected;
        public event EventHandler<ClientDisconnectedEventArgs> ClientDisconnected;
        public void Connect(string ip, string port, string protocol);
        public void Send(string login);
    }
}
