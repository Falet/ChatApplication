using System;
using System.Collections.Generic;
using System.Text;

namespace Client.Model
{
    public class AddedNewChatModelEventArgs
    {
        #region Properties

        public int NumberChat { get; }
        public List<string> Clients { get; }

        #endregion Properties


        #region Constructors

        public AddedNewChatModelEventArgs(int numberChat, List<string> clients)
        {
            NumberChat = numberChat;
            Clients = clients;
        }

        #endregion Constructors
    }
}
