namespace TestServer.Network
{
    using System;
    interface IConnection
    {
        #region Events

        public event EventHandler<ConnectionStateChangedEventArgs> ConnectionStateChanged;
        public event EventHandler<MessageReceivedEventArgs> MessageReceived;

        #endregion Events

        public void Send();

    }
}
