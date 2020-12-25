
using System;
using System.Collections.Generic;
using System.Text;

namespace Client.Model
{
    using Common.Network.Packets;
    using Common.Network;
    public interface IHandlerConnection
    {
        public event EventHandler<ClientConnectedToServerEventArgs> ClientConnected;
        public event EventHandler<ClientDisconnectedEventArgs> ClientDisconnected;
        public void Connect(string ip, string port, string protocol);
        public void Send(string login);
    }
}
