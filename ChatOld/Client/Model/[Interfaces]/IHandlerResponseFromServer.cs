using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Network;
using Common.Network.Packets;

namespace Client.Model
{
    public interface IHandlerResponseFromServer
    {
        public event EventHandler<ClientConnectedToServerEventArgs> ClientConnected;
        public event EventHandler<AnotherClientConnectedEventArgs> AnotherClientConnected;
        public event EventHandler<MessageReceivedForVMEventArgs> MessageReceived;
        public event EventHandler<ClientConnectedToChatEventArgs> ConnectedToChat;
        public event EventHandler<AddedNewChatModelEventArgs> AddedChat;
        public event EventHandler<AddedClientsToChatEventArgs> AddedClientsToChat;
        public event EventHandler<RemovedClientsFromChatEventArgs> RemovedClientsFromChat;
        public event EventHandler<ClientDisconnectedEventArgs> AnotherClientDisconnected;
        public event EventHandler<NumbersOfChatsReceivedEventArgs> ResponseNumbersChats;
        public event EventHandler<ReceivedInfoAboutAllClientsEventArgs> ReceivedInfoAboutAllClients;
        public void ParsePacket(MessageContainer container);
    }
}
