using System;
using System.Collections.Generic;
using System.Text;

namespace Client.Model
{
    public class AddedChatEventArgs
    {
        #region Properties

        public string NameOfClientCreator { get; }

        public Dictionary<string, bool> NameOfClientsForAdd { get; }

        public int NumberChat { get; }

        public Dictionary<string, bool> AccessNameClientForAdd { get; }

        #endregion Properties

        #region Constructors

        public AddedChatEventArgs(string nameofClientCreator, Dictionary<string, bool> nameOfClients, Dictionary<string, bool> accessNameClientForAdd, int numberChat)
        {
            NameOfClientCreator = nameofClientCreator;
            NameOfClientsForAdd = nameOfClients;
            AccessNameClientForAdd = accessNameClientForAdd;
            NumberChat = numberChat;
        }

        #endregion Constructors
    }
}
