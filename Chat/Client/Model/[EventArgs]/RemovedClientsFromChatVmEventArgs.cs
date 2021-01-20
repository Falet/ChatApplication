namespace Client.Model.Event
{
    using System.Collections.Generic;

    public class RemovedClientsFromChatVmEventArgs
    {
        #region Properties

        public string NameOfRemover { get; }

        public Dictionary<string, bool> Clients { get; }

        public int NumberChat { get; }

        #endregion Properties

        #region Constructors

        public RemovedClientsFromChatVmEventArgs(string clientName, int numberChat, Dictionary<string, bool> clients)
        {
            NameOfRemover = clientName;
            Clients = clients;
            NumberChat = numberChat;
        }

        #endregion Constructors
    }
}
