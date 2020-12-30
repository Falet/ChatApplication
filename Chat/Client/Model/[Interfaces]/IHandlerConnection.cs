
using System;
using System.Collections.Generic;
using System.Text;

namespace Client.Model
{
    using Common.Network.Packets;
    using Common.Network;
    public interface IHandlerConnection
    {
        public Dictionary<string, bool> InfoClientsAtChat { get; }
        public event EventHandler<ClientConnectedToServerEventArgs> ClientConnected;
        public event EventHandler<AnotherClientConnectedEventArgs> AnotherClientConnected;
        public event EventHandler<ClientDisconnectedEventArgs> AnotherClientDisconnected;
        public event EventHandler<ReceivedInfoAboutAllClientsEventArgs> ReceivedInfoAboutAllClients;
        public void Connect(string ip, string port, string protocol);
        public void Send(string login);
    }
}
