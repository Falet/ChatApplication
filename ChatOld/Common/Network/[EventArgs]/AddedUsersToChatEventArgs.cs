namespace Common.Network
{
    using System.Collections.Generic;
    public class AddedClientsToChatEventArgs
    {
        #region Properties

        public string NameOfClientSender { get; }

        public List<string> Clients { get; }

        public int NumberChat { get; }

        #endregion Properties

        #region Constructors

        public AddedClientsToChatEventArgs(string nameClient, int numberChat, List<string> nameOfClients)
        {
            NameOfClientSender = nameClient;
            Clients = nameOfClients;
            NumberChat = numberChat;
        }

        #endregion Constructors
    }
}
