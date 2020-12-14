namespace Common.Network.Packets
{
    public class ConnectToChatRequest
    {
        #region Properties

        public string ClientName { get; }

        public int NumberChat { get; }

        #endregion Properties

        #region Constructors

        public ConnectToChatRequest(string clientName, int numberChat)
        {
            ClientName = clientName;
            NumberChat = numberChat;
        }

        #endregion Constructors

        #region Methods

        public MessageContainer GetContainer()
        {
            var container = new MessageContainer
            {
                Identifier = nameof(ConnectToChatRequest),
                Payload = this
            };

            return container;
        }

        #endregion Methods
    }
}
