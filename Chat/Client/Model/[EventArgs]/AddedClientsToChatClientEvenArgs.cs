namespace Client.Model
{
    using System.Collections.Generic;

    public class AddedClientsToChatClientEvenArgs
    {
        #region Properties

        public Dictionary<string, bool> NameOfClients { get; }

        public int NumberChat { get; }

        #endregion Properties

        #region Constructors

        public AddedClientsToChatClientEvenArgs(int numberChat, Dictionary<string, bool> nameOfClients)
        {
            NameOfClients = nameOfClients;
            NumberChat = numberChat;
        }

        #endregion Constructors
    }
}
