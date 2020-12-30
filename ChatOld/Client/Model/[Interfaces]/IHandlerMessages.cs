using System;
using System.Collections.Generic;
using System.Text;

namespace Client.Model
{
    using Common.Network.Packets;
    using Common.Network;
    public interface IHandlerMessages
    {
        public event EventHandler<MessageReceivedForVMEventArgs> MessageReceived;
        public event EventHandler<ClientConnectedToChatEventArgs> ConnectedToChat;
        public void SendMessage(string message, int numberChat);
        public void ConnectToChat(int numberChat);
    }
}
