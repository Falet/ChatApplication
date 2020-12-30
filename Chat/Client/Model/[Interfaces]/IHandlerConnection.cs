
using System;
using System.Collections.Generic;
using System.Text;

namespace Client.Model
{
    using Common.Network.Packets;
    using Common.Network;
    public interface IHandlerConnection
    {
        Dictionary<string, bool> InfoClientsAtChat { get; }
        event EventHandler<ClientConnectedToServerEventArgs> ClientConnected;
        event EventHandler<AnotherClientConnectedEventArgs> AnotherClientConnected;
        event EventHandler<ClientDisconnectedEventArgs> AnotherClientDisconnected;
        event EventHandler<ReceivedInfoAboutAllClientsEventArgs> ReceivedInfoAboutAllClients;
        void Connect(string ip, string port, string protocol);
        void Send(string login);
    }
}
