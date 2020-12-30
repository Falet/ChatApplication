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
    }
}
