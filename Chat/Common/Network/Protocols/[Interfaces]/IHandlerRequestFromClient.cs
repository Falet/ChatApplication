using Common.Network.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Network
{
    public interface IHandlerRequestFromClient
    {
        event EventHandler<ClientConnectedEventArgs> ClientConnected;
        event EventHandler<MessageReceivedEventArgs> MessageReceived;
        event EventHandler<ConnectionToChatEventArgs> ConnectedToChat;
        event EventHandler<AddedNewChatEventArgs> AddedChat;
        event EventHandler<RemovedChatEventArgs> RemovedChat;
        event EventHandler<AddedClientsToChatEventArgs> AddedClientsToChat;
        event EventHandler<RemovedClientsFromChatEventArgs> RemovedClientsFromChat;
        event EventHandler<ClientDisconnectedEventArgs> ClientDisconnected;
        event EventHandler<ClientRequestedNumbersChatEventArgs> RequestNumbersChats;
        event EventHandler<InfoAboutAllClientsEventArgs> RequestInfoAllClient;
        void ParsePacket(Guid clientId, MessageContainer container);
    }
}
