namespace Client.Model
{
    using System;
    using System.Collections.Generic;

    public interface IHandlerConnection
    {
        #region Properties

        Dictionary<string, bool> InfoClientsAtChat { get; }

        #endregion Properties

        #region Event

        event EventHandler<ClientConnectedToServerEventArgs> ClientConnected;
        event EventHandler<AnotherClientConnectedEventArgs> AnotherClientConnected;
        event EventHandler<AnotherClientDisconnectedEventArgs> AnotherClientDisconnected;
        event EventHandler<ReceivedInfoAboutAllClientsEventArgs> ReceivedInfoAboutAllClients;
        event EventHandler<AnotherClientConnectedEventArgs> AnotherNewClientConnected;

        #endregion Event

        #region Methods

        void Connect(string ip, string port, string protocol);
        void Send(string login);

        #endregion Methods
    }
}
