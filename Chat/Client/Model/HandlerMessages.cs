using Common.Network;
using System;
using System.Collections.Generic;
using System.Text;

namespace Client.Model
{
    public class HandlerMessages : IHandlerMessages
    {
        public event EventHandler<MessageReceivedEventArgs> MessageReceived;
        public event EventHandler<ConnectionToChatEventArgs> ConnectedToChat;

        public void ConnectToChat(int numberChat)
        {

        }

        public void Send(string message, int numberChat)
        {

        }
    }
}
