namespace Common.Network
{
    public class RemovedChatEventArgs
    {
        #region Properties

        public string NameOfClient { get; }

        public int NumberChat { get; }

        #endregion Properties

        #region Constructors
        
        public RemovedChatEventArgs(string nameOfClient,int numberChat)
        {
            NameOfClient = nameOfClient;
            NumberChat = numberChat;
        }

        #endregion Constructors
    }
}
