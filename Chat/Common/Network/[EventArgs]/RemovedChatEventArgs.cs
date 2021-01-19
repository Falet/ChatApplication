namespace Common.Network
{
    public class RemovedChatEventArgs
    {
        #region Properties

        public string NameClient { get; }

        public int NumberChat { get; }

        #endregion Properties

        #region Constructors
        
        public RemovedChatEventArgs(string nameClient,int numberChat)
        {
            NameClient = nameClient;
            NumberChat = numberChat;
        }

        #endregion Constructors
    }
}
