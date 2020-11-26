using System;
using System.Collections.Generic;
using System.Text;

namespace TestServer
{
    interface IConnection
    {
        #region Events

        event EventHandler<ConnectionStateChangedEventArgs> ConnectionStateChanged;
        event EventHandler<MessageReceivedEventArgs> MessageReceived;

        #endregion Events
        public void Send();
    }
}
