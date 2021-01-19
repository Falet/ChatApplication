namespace Client.Model
{
    using Common.Network;

    public class MessageReceivedForVMEventArgs
    {
        #region Properties

        public MessageInfo Message { get; }

        public int NumberChat { get; }

        #endregion Properties

        #region Constructors

        public MessageReceivedForVMEventArgs(MessageInfo message, int numberChat)
        {
            Message = message;
            NumberChat = numberChat;
        }

        #endregion Constructors
    }
}
