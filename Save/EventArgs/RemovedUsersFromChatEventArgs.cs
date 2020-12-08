using System;
using System.Collections.Generic;
using System.Text;

namespace TestServer.Network
{
    public class RemovedUsersFromChatEventArgs
    {
        #region Properties
        public string ClientName { get; }

        public List<string> Users { get; }

        public int Room { get; }

        #endregion Properties

        #region Constructors

        public RemovedUsersFromChatEventArgs(string clientName, int room, List<string> users)
        {
            ClientName = clientName;
            Users = users;
            Room = room;
        }

        #endregion Constructors
    }
}

