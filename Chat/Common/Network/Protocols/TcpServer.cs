namespace Common.Network
{
    using System.Net;
    using System;
    using System.Collections.Generic;
    using Packets;
    public class TcpServer : ITransportServer
    {

        #region Event

        public event EventHandler<ClientConnectedEventArgs> ClientConnected;
        public event EventHandler<MessageReceivedEventArgs> MessageReceived;
        public event EventHandler<ConnectionToChatEventArgs> ConnectedToChat;
        public event EventHandler<AddedChatEventArgs> AddedChat;
        public event EventHandler<RemovedChatEventArgs> RemovedChat;
        public event EventHandler<AddedClientsToChatEventArgs> AddedClientsToChat;
        public event EventHandler<RemovedClientsFromChatEventArgs> RemovedClientsFromChat;
        public event EventHandler<ClientDisconnectedEventArgs> ClientDisconnected;

        #endregion Event

        #region Constructors

        public TcpServer(IPEndPoint IPendPoint)
        {

        }

        #endregion Constructors

        #region Methods

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
            var buf = message.Payload.GetType();
        }

        #endregion Methods
    }
}
