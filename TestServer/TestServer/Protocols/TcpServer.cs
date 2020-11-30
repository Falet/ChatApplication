using System;
using System.Collections.Generic;
using System.Text;

namespace TestServer.Network
{
    using System.Net;
    public class TcpServer : ITransport
    {

        #region Event

        public event EventHandler<ConnectionStateChangedEventArgs> ConnectionStateChanged;
        public event EventHandler<MessageReceivedEventArgs> MessageReceived;
        public event EventHandler<ConnectionToChatEventArgs> ConnectionToChat;

        #endregion Event

        #region Constructors

        public TcpServer()
        {

        }

        #endregion Constructors

        public void Start(IPEndPoint IPendPoint)
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
    }
}
