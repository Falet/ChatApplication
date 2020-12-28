using System;
using System.Collections.Generic;
using System.Text;

namespace Client.Model
{
    public class NumbersOfChatsReceivedEventArgs
    {
        #region Properties

        public Dictionary<int, string> NumbersChatsForClient { get; }
        public Dictionary<string, bool> InfoClientsAtChat { get; }

        #endregion Properties

        #region Constructors

        public NumbersOfChatsReceivedEventArgs(Dictionary<string, bool> infoClientsAtChat, Dictionary<int, string> numbersChatsForClient)
        {
            InfoClientsAtChat = infoClientsAtChat;
            NumbersChatsForClient = numbersChatsForClient;
        }

        #endregion Constructors
    }
}
