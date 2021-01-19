namespace Common.Network
{
    public class MessageReceivedEventArgs
    {
        #region Properties

        public string NameOfClient { get; }

        public string Message { get; }

        public int NumberChat { get; }

        #endregion Properties

        #region Constructors

        public MessageReceivedEventArgs(string nameOfClient, string message, int numberChat)
        {
            NameOfClient = nameOfClient;
            Message = message;
            NumberChat = numberChat;
        }

        #endregion Constructors
    }
}
