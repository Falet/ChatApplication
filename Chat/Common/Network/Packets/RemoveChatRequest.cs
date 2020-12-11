namespace Common.Network.Packets
{
    public class RemoveChatRequest
    {
        #region Properties

        public string NameOfRemover { get; }

        public int NumberChat { get; }

        #endregion Properties


        #region Constructors

        public RemoveChatRequest(string remover, int numberChat)
        {
            NameOfRemover = remover;
            NumberChat = numberChat;
        }

        #endregion Constructors

        #region Methods

        public MessageContainer GetContainer()
        {
            var container = new MessageContainer
            {
                Identifier = nameof(RemoveChatRequest),
                Payload = this
            };

            return container;
        }

        #endregion Methods
    }
}
