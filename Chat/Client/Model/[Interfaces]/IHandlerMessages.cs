using System;
using System.Collections.Generic;
using System.Text;

namespace Client.Model
{
    using Common.Network.Packets;
    using Common.Network;
    public interface IHandlerMessages
    {
        public event EventHandler<MessageReceivedEventArgs> MessageReceived;
        public event EventHandler<ConnectionToChatEventArgs> ConnectedToChat;
        public void Send(string message);
    }
}
