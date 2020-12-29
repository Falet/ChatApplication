namespace Common.Network.Packets
{
    public class MessageResponse
    {
        #region Properties

        public string FromClientName { get; }
        public string Message { get; }
        public int NumberChat { get; }

        #endregion Properties

        #region Constructors

        public MessageResponse(string clientName, string message, int numberChat)
        {
            FromClientName = clientName;
            Message = message;
            NumberChat = numberChat;
        }

        #endregion Constructors
    }
}
