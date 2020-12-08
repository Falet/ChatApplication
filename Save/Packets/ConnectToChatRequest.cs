namespace TestServer.Network
{
    public class ConnectToChatRequest
    {
        #region Properties

        public string ClientName { get; }

        public int Room { get; }

        #endregion Properties

        #region Constructors

        public ConnectToChatRequest(string clientName, int room)
        {
            ClientName = clientName;
            Room = room;
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
