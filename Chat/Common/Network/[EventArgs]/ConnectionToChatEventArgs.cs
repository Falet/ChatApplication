namespace Common.Network
{
    public class ConnectionToChatEventArgs
    {
        #region Properties

        public string NameClient { get; }

        public int NumberChat { get; }

        #endregion Properties

        #region Constructors

        public ConnectionToChatEventArgs(string nameClient, int numberChat)
        {
            NameClient = nameClient;
            NumberChat = numberChat;
        }

        #endregion Constructors
    }
}
