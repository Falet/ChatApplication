namespace Common.Network.Packets
{
    using System.Collections.Generic;
    public class AddNewChatResponse
    {
        #region Properties

        public int NumberChat { get; }
        public List<string> Clients { get; }

        #endregion Properties


        #region Constructors

        public AddNewChatResponse(int numberChat, List<string> clients)
        {
            NumberChat = numberChat;
            Clients = clients;
        }

        #endregion Constructors

        #region Methods

        public MessageContainer GetContainer()
        {
            var container = new MessageContainer
            {
                Identifier = nameof(AddNewChatResponse),
                Payload = this
            };

            return container;
        }

        #endregion Methods
    }
}
