using System;
using System.Collections.Generic;
using System.Text;

namespace TestServer.Network
{
    public class ConnectionToChatEventArgs
    {
        #region Properties

        public string ClientName { get; }

        public int NumberRoom { get; }

        #endregion Properties

        #region Constructors

        public ConnectionToChatEventArgs(string clientName, int room)
        {
            ClientName = clientName;
            NumberRoom = room;
        }

        #endregion Constructors
    }
}
