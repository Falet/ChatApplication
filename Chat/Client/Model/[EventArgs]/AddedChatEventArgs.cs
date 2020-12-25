using System;
using System.Collections.Generic;
using System.Text;

namespace Client.Model
{
    public class AddedChatEventArgs
    {
        #region Properties

        public string NameOfClientCreator { get; }

        public List<string> NameOfClientsForAdd { get; }

        public int NumberChat { get; }

        #endregion Properties

        #region Constructors

        public AddedChatEventArgs(string nameofClientCreator, List<string> nameOfClients, int numberChat)
        {
            NameOfClientCreator = nameofClientCreator;
            NameOfClientsForAdd = nameOfClients;
            NumberChat = numberChat;
        }

        #endregion Constructors
    }
}
