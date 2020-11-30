

namespace TestServer.Network
{
    using System;
    using System.Net;

    public interface ITransport
    {

        #region Events

        public event EventHandler<ConnectionStateChangedEventArgs> ConnectionStateChanged;
        public event EventHandler<MessageReceivedEventArgs> MessageReceived;
        public event EventHandler<ConnectionToChatEventArgs> ConnectionToChat;

        #endregion Events

        #region Methods

        public void Start(IPEndPoint IPendPoint);

        public void Stop();

        public void AddConnection();

        public void FreeConnection();

        public void Send();

        #endregion Methods
    }
}
