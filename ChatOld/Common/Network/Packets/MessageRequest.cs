namespace Common.Network.Packets
{
    public class MessageRequest
    {
        #region Properties

        public string ClientName { get; }
        public string Message { get; }
        public int NumberChat { get; }

        #endregion Properties

        #region Constructors

        public MessageRequest(string clientName,string message, int numberChat)
        {
            ClientName = clientName;
            Message = message;
            NumberChat = numberChat;
        }

        #endregion Constructors
    }
}
