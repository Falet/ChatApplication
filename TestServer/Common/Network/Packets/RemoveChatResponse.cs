namespace Common.Network.Packets
{
    public class RemoveChatResponse
    {
        #region Properties

        public int NumberChat { get; }

        #endregion Properties


        #region Constructors

        public RemoveChatResponse(int numberChat)
        {
            NumberChat = numberChat;
        }

        #endregion Constructors

        #region Methods

        public MessageContainer GetContainer()
        {
            var container = new MessageContainer
            {
                Identifier = nameof(RemoveChatResponse),
                Payload = this
            };

            return container;
        }

        #endregion Methods
    }
}
