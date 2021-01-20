namespace Client.Model.Event
{
    using Common.Network;
    using System.Collections.Generic;

    public class ClientConnectedToChatVmEventArgs
    {
        #region Properties

        public int NumberChat { get; }
        public List<MessageInfo> AllMessageFromChat { get; }

        #endregion Properties


        #region Constructors

        public ClientConnectedToChatVmEventArgs(List<MessageInfo> allMessageFromChat, int numberChat)
        {
            AllMessageFromChat = allMessageFromChat;
            NumberChat = numberChat;
        }

        #endregion Constructors
    }
}
