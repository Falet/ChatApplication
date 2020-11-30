using System;
using System.Collections.Generic;
using System.Text;

namespace TestServer.Network
{
    public class HandlerRequest
    {
        public event EventHandler<ConnectionStateChangedEventArgs> NewUserConnected;
        public event EventHandler<MessageReceivedEventArgs> NewMessageRecieved;
        public HandlerRequest(ITransport server)
        {
            server.ConnectionStateChanged += OnConnection;
            server.MessageReceived += OnMessage;
            server.ConnectionToChat += OnChatOpened;
        }
        public void OnConnection(object sender, ConnectionStateChangedEventArgs container)
        {

        }
        public void OnMessage(object sender, MessageReceivedEventArgs container)
        {

        }
        public void OnChatOpened(object sender, ConnectionToChatEventArgs container)
        {

        }
    }
}
