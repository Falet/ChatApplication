using System;
using System.Collections.Generic;
using System.Text;

namespace TestServer.Network
{
    public class UserDisconnectedEventArgs
    {
        public string ClientName { get; }

        public UserDisconnectedEventArgs(string clientName)
        {
            ClientName = clientName;
        }
    }
}
