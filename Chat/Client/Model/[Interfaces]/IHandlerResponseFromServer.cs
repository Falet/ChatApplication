namespace Client.Model
{
    using System;
    using Common.Network;
    using Common.Network.Packets;

    public interface IHandlerResponseFromServer
    {
        #region Event

        event EventHandler<ClientConnectedToServerEventArgs> ClientConnected;
        event EventHandler<AnotherClientConnectedEventArgs> AnotherClientConnected;
        event EventHandler<AnotherClientDisconnectedEventArgs> AnotherClientDisconnected;
        event EventHandler<AddedNewChatModelEventArgs> AddedChat;
        event EventHandler<RemovedChatEventArgs> RemovedChat;
        event EventHandler<AddedClientsToChatEventArgs> AddedClientsToChat;
        event EventHandler<RemovedClientsFromChatEventArgs> RemovedClientsFromChat;
        event EventHandler<ClientConnectedToChatEventArgs> ConnectedToChat;
        event EventHandler<MessageReceivedForVMEventArgs> MessageReceived;
        event EventHandler<NumbersOfChatsReceivedEventArgs> ResponseNumbersChats;
        event EventHandler<ReceivedInfoAboutAllClientsEventArgs> ReceivedInfoAboutAllClients;

        #endregion Event

        #region Methods

        void ParsePacket(MessageContainer container);

        #endregion Methods
    }
}
