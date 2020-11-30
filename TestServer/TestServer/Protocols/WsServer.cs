using System;
using System.Collections.Generic;
using System.Text;

namespace TestServer.Network
{
    using System.Collections.Concurrent;
    using System.Net;

    using Newtonsoft.Json.Linq;

    using WebSocketSharp.Server;
    public class WsServer: ITransport
    {
        #region Fields

        private readonly IPEndPoint _listenAddress;
        private readonly ConcurrentDictionary<Guid, WsConnection> _connections;

        private WebSocketServer _server;

        #endregion Fields

        #region Event

        public event EventHandler<ConnectionStateChangedEventArgs> ConnectionStateChanged;
        public event EventHandler<MessageReceivedEventArgs> MessageReceived;
        public event EventHandler<ConnectionToChatEventArgs> ConnectionToChat;

        #endregion Event

        #region Constructors

        public WsServer()
        {
            this.SetDictionaryOfUsers(RequestManagerDb.GetAllNameUser());
        }

        #region Constructors

        #region Methods

        public void Start()
        {

        }

        public void Stop()
        {

        }

        public void SetDictionaryOfUsers(List<string> listNameOfUsers)
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

        #endregion Methods
    }
}
