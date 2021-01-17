namespace Client.Model
{
    using Common.Network;
    using System.Collections.Generic;

    public class ClientConnectedToChatEventArgs
    {
        #region Properties

        public int NumberChat { get; }
        public List<MessageInfo> AllMessageFromChat { get; }

        #endregion Properties


        #region Constructors

        public ClientConnectedToChatEventArgs(List<MessageInfo> allMessageFromChat, int numberChat)
        {
            AllMessageFromChat = allMessageFromChat;
            NumberChat = numberChat;
        }

        #endregion Constructors
    }
}
