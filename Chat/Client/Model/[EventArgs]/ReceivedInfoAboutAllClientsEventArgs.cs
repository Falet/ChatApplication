namespace Client.Model
{
    using System.Collections.Generic;

    public class ReceivedInfoAboutAllClientsEventArgs
    {
        #region Properties

        public Dictionary<string, bool> InfoClientsAtChat { get; }

        #endregion Properties

        #region Constructors

        public ReceivedInfoAboutAllClientsEventArgs(Dictionary<string, bool> infoClientsAtChat)
        {
            InfoClientsAtChat = infoClientsAtChat;
        }

        #endregion Constructors
    }
}
