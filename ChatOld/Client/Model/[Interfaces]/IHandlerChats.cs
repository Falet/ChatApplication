using System;
using System.Collections.Generic;
using System.Text;

namespace Client.Model
{
    using Common.Network;
    public interface IHandlerChats
    {
        public event EventHandler<AddedChatEventArgs> AddedChat;
        public event EventHandler<AddedClientsToChatClientEvenArgs> AddedClientsToChat;
        public event EventHandler<RemovedClientsFromChatForVMEventArgs> RemovedClientsFromChat;
        public event EventHandler<NumbersOfChatsReceivedEventArgs> ResponseNumbersChats;

        public void AddChat(List<string> namesClientForAdd);
        public void AddClientToChat(int numberChat, List<string> namesOfClients);
        public void RemoveClientFromChat(int numberChat, List<string> namesOfClients);
    }
}
