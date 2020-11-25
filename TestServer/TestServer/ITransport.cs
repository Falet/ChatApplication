using System;
using System.Collections.Generic;
using System.Text;

namespace TestServer.Network
{
    interface ITransport
    {
        #region Events

        event EventHandler<ConnectionStateChangedEventArgs> ConnectionStateChanged;
        event EventHandler<MessageReceivedEventArgs> MessageReceived;

        #endregion Events

        #region Methods

        void Start();

        void Stop();

        void AddConnection();

        void FreeConnection();

        #endregion Methods

    }
}
