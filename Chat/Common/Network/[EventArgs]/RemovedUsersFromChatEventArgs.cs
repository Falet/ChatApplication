namespace Common.Network
{
    using System.Collections.Generic;
    public class RemovedClientsFromChatEventArgs
    {
        #region Properties

        public string NameOfRemover { get; }

        public List<string> NameOfClients { get; }

        public int NumberChat { get; }

        #endregion Properties

        #region Constructors

        public RemovedClientsFromChatEventArgs(string clientName, int numberChat, List<string> clients)
        {
            NameOfRemover = clientName;
            NameOfClients = clients;
            NumberChat = numberChat;
        }

        #endregion Constructors
    }
}

