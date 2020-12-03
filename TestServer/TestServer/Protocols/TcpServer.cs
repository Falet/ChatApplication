using System;
using System.Collections.Generic;
using System.Text;

namespace TestServer.Network
{
    using System.Net;
    public class TcpServer : ITransport
    {

        #region Event

        public event EventHandler<UserConnectedEventArgs> UserConnected;
        public event EventHandler<MessageReceivedEventArgs> MessageReceived;
        public event EventHandler<ConnectionToChatEventArgs> ConnectedToChat;
        public event EventHandler<AddedChatEventArgs> AddedChat;
        public event EventHandler<RemovedChatEventArgs> RemovedChat;
        public event EventHandler<AddedUsersToChatEventArgs> AddedUsersToChat;
        public event EventHandler<RemovedUsersFromChatEventArgs> RemovedUsersFromChat;
        public event EventHandler<UserDisconnectedEventArgs> UserDisconnected;

        #endregion Event

        #region Constructors

        public TcpServer(IPEndPoint IPendPoint)
        {

        }

        #endregion Constructors

        public void Start()
        {

        }

        public void Stop()
        {

        }

        public void FreeConnection(Guid ClientId)
        {

        }

        public void Send(List<Guid> ListClientId, MessageContainer message)
        {

        }
    }
}
