namespace Common.Network.Packets
{
    public class DisconnectRequest
    {
        #region Properties

        public string ClientName { get; }

        #endregion Properties


        #region Constructors

        public DisconnectRequest(string clientName)
        {
            ClientName = clientName;
        }

        #endregion Constructors

        #region Methods

        public MessageContainer GetContainer()
        {
            var container = new MessageContainer
            {
                Identifier = nameof(DisconnectRequest),
                Payload = this
            };

            return container;
        }

        #endregion Methods
    }
}
