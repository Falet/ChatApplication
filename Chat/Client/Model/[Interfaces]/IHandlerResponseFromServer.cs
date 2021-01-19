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
        event EventHandler<ClientConnectedToServerEventArgs> ClientConnected;
        event EventHandler<AnotherClientConnectedEventArgs> AnotherClientConnected;
        event EventHandler<MessageReceivedForVMEventArgs> MessageReceived;
        event EventHandler<ClientConnectedToChatEventArgs> ConnectedToChat;
        event EventHandler<AddedNewChatModelEventArgs> AddedChat;
        event EventHandler<AddedClientsToChatEventArgs> AddedClientsToChat;
        event EventHandler<RemovedClientsFromChatEventArgs> RemovedClientsFromChat;
        event EventHandler<AnotherClientDisconnectedEventArgs> AnotherClientDisconnected;
        event EventHandler<NumbersOfChatsReceivedEventArgs> ResponseNumbersChats;
        event EventHandler<ReceivedInfoAboutAllClientsEventArgs> ReceivedInfoAboutAllClients;
        event EventHandler<RemovedChatEventArgs> RemovedChat;
        void ParsePacket(MessageContainer container);
    }
}
