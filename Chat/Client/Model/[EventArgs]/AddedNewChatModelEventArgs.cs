namespace Client.Model.Event
{
    using System.Collections.Generic;
    public class AddedNewChatModelEventArgs
    {
        #region Properties

        public string ClientCreator { get; }
        public int NumberChat { get; }
        public List<string> Clients { get; }

        #endregion Properties


        #region Constructors

        public AddedNewChatModelEventArgs(string clientCreator, int numberChat, List<string> clients)
        {
            ClientCreator = clientCreator;
            NumberChat = numberChat;
            Clients = clients;
        }

        #endregion Constructors
    }
}
