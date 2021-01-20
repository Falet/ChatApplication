namespace Client.Model
{
    using System.Collections.Generic;
    public class AddedChatVmEventArgs
    {
        #region Properties

        public string NameOfClientCreator { get; }

        public Dictionary<string, bool> NameOfClientsForAdd { get; }

        public int NumberChat { get; }

        public Dictionary<string, bool> AccessNameClientForAdd { get; }

        #endregion Properties

        #region Constructors

        public AddedChatVmEventArgs(string nameofClientCreator, Dictionary<string, bool> nameOfClients, Dictionary<string, bool> accessNameClientForAdd, int numberChat)
        {
            NameOfClientCreator = nameofClientCreator;
            NameOfClientsForAdd = nameOfClients;
            AccessNameClientForAdd = accessNameClientForAdd;
            NumberChat = numberChat;
        }

        #endregion Constructors
    }
}
