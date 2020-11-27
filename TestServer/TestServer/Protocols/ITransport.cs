using System;
using System.Collections.Generic;
using System.Text;
namespace TestServer.Network
{
    public interface ITransport
    {
        #region Events

        public event EventHandler<ConnectionStateChangedEventArgs> ConnectionStateChanged;
        public event EventHandler<MessageReceivedEventArgs> MessageReceived;
        public event EventHandler<ConnectionToChatEventArgs> ConnectionToChat;

        #endregion Events

        #region Fields

        #endregion Fields

        #region Methods

        public void Start();

        public void Stop();

        public void SetDictionaryOfUsers(List<string> listNameOfUsers);

        public void AddConnection();

        public void FreeConnection();

        public void Send();

        #endregion Methods
    }
}
