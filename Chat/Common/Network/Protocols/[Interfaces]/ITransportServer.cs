namespace Common.Network
{
    using System;
    using System.Collections.Generic;
    using Packets;

    public interface ITransportServer
    {
        #region Events

        public event EventHandler<ClientConnectedEventArgs> ClientConnected;
        public event EventHandler<ClientDisconnectedEventArgs> ClientDisconnected;
        public event EventHandler<MessageReceivedEventArgs> MessageReceived;
        public event EventHandler<ConnectionToChatEventArgs> ConnectedToChat;
        public event EventHandler<AddedChatEventArgs> AddedChat;
        public event EventHandler<RemovedChatEventArgs> RemovedChat;
        public event EventHandler<AddedClientsToChatEventArgs> AddedClientsToChat;
        public event EventHandler<RemovedClientsFromChatEventArgs> RemovedClientsFromChat;

        #endregion Events

        #region Methods

        public void Start();

        public void Stop();

        public void FreeConnection(Guid ClientId);

        public void Send(List<Guid> ListClientId, MessageContainer message);

        public void SendAll(MessageContainer message);

        #endregion Methods
    }
}
