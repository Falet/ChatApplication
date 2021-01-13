namespace Common.Network.Packets
{
    using System.Collections.Generic;
    public class AddNewChatResponse
    {
        #region Properties
        public string ClientCreator { get; }
        public int NumberChat { get; }
        public List<string> Clients { get; }

        #endregion Properties


        #region Constructors

        public AddNewChatResponse(string clientCreator,int numberChat, List<string> clients)
        {
            ClientCreator = clientCreator;
            NumberChat = numberChat;
            Clients = clients;
        }

        #endregion Constructors
    }
}
