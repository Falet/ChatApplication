namespace Common.Network
{
    public class MessageReceivedEventArgs
    {
        #region Properties

        public string NameClient { get; }

        public string Message { get; }

        public int NumberChat { get; }

        #endregion Properties

        #region Constructors

        public MessageReceivedEventArgs(string nameClient, string message, int numberChat)
        {
            NameClient = nameClient;
            Message = message;
            NumberChat = numberChat;
        }

        #endregion Constructors
    }
}
