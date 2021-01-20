namespace Client.Model.Event
{
    using Common.Network;

    public class MessageReceivedVmEventArgs
    {
        #region Properties

        public MessageInfo Message { get; }

        public int NumberChat { get; }

        #endregion Properties

        #region Constructors

        public MessageReceivedVmEventArgs(MessageInfo message, int numberChat)
        {
            Message = message;
            NumberChat = numberChat;
        }

        #endregion Constructors
    }
}
