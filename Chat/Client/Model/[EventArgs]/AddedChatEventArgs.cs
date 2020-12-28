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

        #endregion Properties

        #region Constructors

        public AddedChatEventArgs(string nameofClientCreator, Dictionary<string, bool> nameOfClients, int numberChat)
        {
            NameOfClientCreator = nameofClientCreator;
            NameOfClientsForAdd = nameOfClients;
            NumberChat = numberChat;
        }

        #endregion Constructors
    }
}
