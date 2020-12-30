namespace Common.Network.Packets
{
    public class MessageResponse
    {
        #region Properties

        public MessageInfo Message { get; }
        public int NumberChat { get; }

        #endregion Properties

        #region Constructors

        public MessageResponse(MessageInfo message, int numberChat)
        {
            Message = message;
            NumberChat = numberChat;
        }

        #endregion Constructors
    }
}
