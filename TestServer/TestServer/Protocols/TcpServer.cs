using System;
using System.Collections.Generic;
using System.Text;

namespace TestServer.Network
{
    public class TcpServer : ITransport
    {
        public event EventHandler<ConnectionStateChangedEventArgs> ConnectionStateChanged;
        public event EventHandler<MessageReceivedEventArgs> MessageReceived;
        public event EventHandler<ConnectionToChatEventArgs> ConnectionToChat;

        public void Start()
        {

        }

        public void Stop()
        {

        }

        public void AddConnection()
        {

        }

        public void FreeConnection()
        {

        }

        public void Send()
        {

        }

        public void SetDictionaryOfUsers(List<string> listNameOfUsers)
        {

        }
    }
}
