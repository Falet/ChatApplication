using System;
using System.Collections.Generic;
using System.Text;

namespace Client.Model
{
    using Common.Network;
    public interface IHandlerChats
    {
        event EventHandler<AddedChatEventArgs> AddedChat;
        event EventHandler<AddedClientsToChatClientEvenArgs> AddedClientsToChat;
        event EventHandler<RemovedClientsFromChatForVMEventArgs> RemovedClientsFromChat;
        event EventHandler<RemovedChatEventArgs> RemovedChat;
        void AddChat(List<string> namesClientForAdd);
        void AddClientToChat(int numberChat, List<string> namesOfClients);
        void RemoveClientFromChat(int numberChat, List<string> namesOfClients);
    }
}
