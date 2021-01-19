using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Network.Packets
{
    public class ConnectionNoticeForClients
    {
        #region Properties

        public string NameClient { get; }

        #endregion Properties

        #region Constructors

        public ConnectionNoticeForClients(string nameClient)
        {
            NameClient = nameClient;
        }

        #endregion Constructors
    }
}
