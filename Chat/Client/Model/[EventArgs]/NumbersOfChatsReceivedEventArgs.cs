using System;
using System.Collections.Generic;
using System.Text;

namespace Client.Model
{
    public class NumbersOfChatsReceivedEventArgs
    {
        #region Properties

        public Dictionary<int, string> NumbersChatsForClient { get; }
        public List<string> ClientsAtChat { get; }

        #endregion Properties

        #region Constructors

        public NumbersOfChatsReceivedEventArgs(Dictionary<int, string> numbersChatsForClient, List<string> clientsAtChat)
        {
            ClientsAtChat = clientsAtChat;
            NumbersChatsForClient = numbersChatsForClient;
        }

        #endregion Constructors
    }
}
