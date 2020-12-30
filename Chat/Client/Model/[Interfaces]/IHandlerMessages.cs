using System;
using System.Collections.Generic;
using System.Text;

namespace Client.Model
{
    using Common.Network.Packets;
    using Common.Network;
    public interface IHandlerMessages
    {
        event EventHandler<MessageReceivedForVMEventArgs> MessageReceived;
        event EventHandler<ClientConnectedToChatEventArgs> ConnectedToChat;
        void SendMessage(string message, int numberChat);
        void ConnectToChat(int numberChat);
    }
}
