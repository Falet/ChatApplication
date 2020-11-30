using System;
using System.Collections.Generic;
using System.Text;

namespace TestServer.Network
{
    public class HandlerRequestFromServer
    {
        #region Fields

        private List<UserProperties> _usersProperties;

        #endregion Fields

        public event EventHandler<ConnectionStateChangedEventArgs> NewUserConnected;
        public event EventHandler<MessageReceivedEventArgs> NewMessageRecieved;
        public HandlerRequestFromServer(ITransport server, List<UserProperties> userProperties)
        {
            server.ConnectionStateChanged += OnConnection;
            server.MessageReceived += OnMessage;
            server.ConnectionToChat += OnChatOpened;

            _usersProperties = userProperties;


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
