namespace Client.Model
{
    using System;
    using System.Collections.Generic;
    using Client.Model.Event;
    using Common.Network;
    public interface IHandlerChats
    {
        #region Event

        event EventHandler<AddedChatVmEventArgs> AddedChat;
        event EventHandler<AddedClientsToChatClientVmEvenArgs> AddedClientsToChat;
        event EventHandler<RemovedClientsFromChatVmEventArgs> RemovedClientsFromChat;
        event EventHandler<RemovedChatEventArgs> RemovedChat;

        #endregion Event

        #region Methods

        void AddChat(List<string> namesClientForAdd);
        void AddClientToChat(int numberChat, List<string> namesOfClients);
        void RemoveClientFromChat(int numberChat, List<string> namesOfClients);

        #endregion Methods
    }
}
