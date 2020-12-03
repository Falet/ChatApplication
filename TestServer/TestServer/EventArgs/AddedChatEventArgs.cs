using System;
using System.Collections.Generic;
using System.Text;

namespace TestServer.Network
{
    public class AddedChatEventArgs
    {
        #region Properties
        public string ClientName { get; }

        public List<string> Users { get; }
        #endregion Properties

        #region Constructors

        public AddedChatEventArgs(string clientName, List<string> users)
        {
            ClientName = clientName;
            Users = users;
        }

        #endregion Constructors
    }
}
