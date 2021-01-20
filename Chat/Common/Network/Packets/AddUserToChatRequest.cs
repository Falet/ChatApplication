namespace Common.Network.Packets
{
    using System.Collections.Generic;
    public class AddClientToChatRequest
    {
        #region Properties

        public string ClientName { get; }

        public List<string> Clients { get; }

        public int NumberChat { get; }

        #endregion Properties

        #region Constructors

        public AddClientToChatRequest(string clientName, List<string> clients, int numberChat)
        {
            ClientName = clientName;
            Clients = clients;
            NumberChat = numberChat;
        }

        #endregion Constructors
    }
}
