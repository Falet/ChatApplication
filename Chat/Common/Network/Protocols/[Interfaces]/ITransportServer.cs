namespace Common.Network
{
    using System;
    using System.Collections.Generic;
    using Packets;

    public interface ITransportServer
    {
        #region Events

        event EventHandler<ClientConnectedEventArgs> ClientConnected;
        event EventHandler<ClientDisconnectedEventArgs> ClientDisconnected;
        event EventHandler<MessageReceivedEventArgs> MessageReceived;
        event EventHandler<ConnectionToChatEventArgs> ConnectedToChat;
        event EventHandler<AddedNewChatEventArgs> AddedChat;
        event EventHandler<RemovedChatEventArgs> RemovedChat;
        event EventHandler<AddedClientsToChatEventArgs> AddedClientsToChat;
        event EventHandler<RemovedClientsFromChatEventArgs> RemovedClientsFromChat; 
        event EventHandler<ClientRequestedNumbersChatEventArgs> RequestNumbersChats;
        #endregion Events

        #region Methods

        void FreeConnection(Guid ClientId);

        void Send(List<Guid> ListClientId, MessageContainer message);

        void SendAll(Guid clientGuid, MessageContainer message);

        #endregion Methods
    }
}
