namespace Common.Network.Packets
{
    using System.Collections.Generic;
    public class ConnectToChatResponse
    {
        #region Properties

        public int NumberChat { get; }
        public List<MessageInfo> AllMessageFromChat { get; }

        #endregion Properties

        #region Constructors

        public ConnectToChatResponse(int numberChat, List<MessageInfo> allMessageFromChat)
        {
            NumberChat = numberChat;
            AllMessageFromChat = allMessageFromChat;
        }

        #endregion Constructors

        #region Methods

        public MessageContainer GetContainer()
        {
            var container = new MessageContainer
            {
                Identifier = nameof(ConnectToChatResponse),
                Payload = this
            };

            return container;
        }

        #endregion Methods
    }
}
