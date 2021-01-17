namespace Client.Model
{
    using System;
    using System.Collections.Generic;
    using Common.Network;
    public interface IHandlerChats
    {
        #region Event

        event EventHandler<AddedChatEventArgs> AddedChat;
        event EventHandler<AddedClientsToChatClientEvenArgs> AddedClientsToChat;
        event EventHandler<RemovedClientsFromChatForVMEventArgs> RemovedClientsFromChat;
        event EventHandler<RemovedChatEventArgs> RemovedChat;

        #endregion Event

        #region Methods

        void AddChat(List<string> namesClientForAdd);
        void AddClientToChat(int numberChat, List<string> namesOfClients);
        void RemoveClientFromChat(int numberChat, List<string> namesOfClients);

        #endregion Methods
    }
}
