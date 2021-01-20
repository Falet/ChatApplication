namespace Client.Model
{
    using System;
    using Client.Model.Event;
    using Common.Network;
    using Common.Network.Packets;

    public interface IHandlerResponseFromServer
    {
        #region Event

        event EventHandler<ClientConnectedToServerVmEventArgs> ClientConnected;
        event EventHandler<AnotherClientConnectedVmEventArgs> AnotherClientConnected;
        event EventHandler<AnotherClientDisconnectedVmEventArgs> AnotherClientDisconnected;
        event EventHandler<AddedNewChatModelEventArgs> AddedChat;
        event EventHandler<RemovedChatEventArgs> RemovedChat;
        event EventHandler<AddedClientsToChatEventArgs> AddedClientsToChat;
        event EventHandler<RemovedClientsFromChatEventArgs> RemovedClientsFromChat;
        event EventHandler<ClientConnectedToChatVmEventArgs> ConnectedToChat;
        event EventHandler<MessageReceivedVmEventArgs> MessageReceived;
        event EventHandler<NumbersOfChatsReceivedModelEventArgs> ResponseNumbersChats;
        event EventHandler<ReceivedInfoAboutAllClientsVmEventArgs> ReceivedInfoAboutAllClients;

        #endregion Event

        #region Methods

        void ParsePacket(MessageContainer container);

        #endregion Methods
    }
}
