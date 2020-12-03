namespace TestServer.Network
{
    using System;
    using System.Collections.Generic;
    using System.Net;

    public interface ITransport
    {

        #region Events

        
        public event EventHandler<UserConnectedEventArgs> UserConnected;
        public event EventHandler<UserDisconnectedEventArgs> UserDisconnected;
        public event EventHandler<MessageReceivedEventArgs> MessageReceived;
        public event EventHandler<ConnectionToChatEventArgs> ConnectedToChat;
        public event EventHandler<AddedChatEventArgs> AddedChat;
        public event EventHandler<RemovedChatEventArgs> RemovedChat;
        public event EventHandler<AddedUsersToChatEventArgs> AddedUsersToChat;
        public event EventHandler<RemovedUsersFromChatEventArgs> RemovedUsersFromChat;
        #endregion Events

        #region Methods

        public void Start();

        public void Stop();

        public void FreeConnection(Guid ClientId);

        public void Send(List<Guid> ListClientId, MessageContainer message);

        #endregion Methods
    }
}
