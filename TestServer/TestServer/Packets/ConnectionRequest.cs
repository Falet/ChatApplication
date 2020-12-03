namespace TestServer.Network
{
    public class ConnectionRequest
    {
        #region Properties

        public string ClientName { get; }

        #endregion Properties

        #region Constructors

        public ConnectionRequest(string clientname)
        {
            ClientName = clientname;
        }

        #endregion Constructors

        #region Methods

        public MessageContainer GetContainer()
        {
            var container = new MessageContainer
            {
                Identifier = nameof(ConnectionRequest),
                Payload = this
            };

            return container;
        }

        #endregion Methods
    }
}
