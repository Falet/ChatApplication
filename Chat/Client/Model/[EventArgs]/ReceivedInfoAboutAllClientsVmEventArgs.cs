namespace Client.Model.Event
{
    using System.Collections.Generic;

    public class ReceivedInfoAboutAllClientsVmEventArgs
    {
        #region Properties

        public Dictionary<string, bool> InfoClientsAtChat { get; }

        #endregion Properties

        #region Constructors

        public ReceivedInfoAboutAllClientsVmEventArgs(Dictionary<string, bool> infoClientsAtChat)
        {
            InfoClientsAtChat = infoClientsAtChat;
        }

        #endregion Constructors
    }
}
