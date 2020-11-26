using System;
using System.Collections.Generic;
using System.Text;

namespace TestServer.Network
{
    interface ITransport
    {
        #region Methods

        void Start();

        void Stop();

        void AddConnection();

        void FreeConnection();

        public void Send();

        #endregion Methods

    }
}
