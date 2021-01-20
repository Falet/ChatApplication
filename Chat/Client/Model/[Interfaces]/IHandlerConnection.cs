namespace Client.Model
{
    using Client.Model.Event;
    using System;
    using System.Collections.Generic;

    public interface IHandlerConnection
    {
        #region Properties

        Dictionary<string, bool> InfoClientsAtChat { get; }

        #endregion Properties

        #region Event

        event EventHandler<ClientConnectedToServerVmEventArgs> ClientConnected;
        event EventHandler<AnotherClientConnectedVmEventArgs> AnotherClientConnected;
        event EventHandler<AnotherClientDisconnectedVmEventArgs> AnotherClientDisconnected;
        event EventHandler<ReceivedInfoAboutAllClientsVmEventArgs> ReceivedInfoAboutAllClients;
        event EventHandler<AnotherClientConnectedVmEventArgs> AnotherNewClientConnected;

        #endregion Event

        #region Methods

        void Connect(string ip, string port, string protocol);
        void Send(string login);

        #endregion Methods
    }
}
