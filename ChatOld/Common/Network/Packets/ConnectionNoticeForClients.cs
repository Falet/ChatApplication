using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Network.Packets
{
    public class ConnectionNoticeForClients
    {
        #region Properties

        public string NameOfClient { get; }

        #endregion Properties

        #region Constructors

        public ConnectionNoticeForClients(string nameOfClient)
        {
            NameOfClient = nameOfClient;
        }

        #endregion Constructors
    }
}
