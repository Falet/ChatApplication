namespace Common.Network
{
    public class ConnectionToChatEventArgs
    {
        #region Properties

        public string NameOfClient { get; }

        public int NumberChat { get; }

        #endregion Properties

        #region Constructors

        public ConnectionToChatEventArgs(string nameOfClient, int numberChat)
        {
            NameOfClient = nameOfClient;
            NumberChat = numberChat;
        }

        #endregion Constructors
    }
}
