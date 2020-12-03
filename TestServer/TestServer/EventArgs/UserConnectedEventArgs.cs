using System;
using System.Collections.Generic;
using System.Text;

namespace TestServer.Network
{
    public class UserConnectedEventArgs
    {
        #region Properties

        public string ClientName { get; }

        public Guid ClientId { get; }

        #endregion Properties

        #region Constructors

        public UserConnectedEventArgs(string clientName, Guid clientId)
        {
            ClientName = clientName;
            ClientId = clientId;
        }

        #endregion Constructors
    }
}