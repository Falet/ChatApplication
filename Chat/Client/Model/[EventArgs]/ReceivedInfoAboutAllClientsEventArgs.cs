using System;
using System.Collections.Generic;
using System.Text;

namespace Client.Model
{
    public class ReceivedInfoAboutAllClientsEventArgs
    {
        #region Properties

        public Dictionary<string, bool> InfoClientsAtChat { get; }

        #endregion Properties

        #region Constructors

        public ReceivedInfoAboutAllClientsEventArgs(Dictionary<string, bool> infoClientsAtChat)
        {
            InfoClientsAtChat = infoClientsAtChat;
        }

        #endregion Constructors
    }
}
