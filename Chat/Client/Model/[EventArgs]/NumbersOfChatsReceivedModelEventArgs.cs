namespace Client.Model.Event
{
    using Common.Network;
    using System.Collections.Generic;

    public class NumbersOfChatsReceivedModelEventArgs
    {
        #region Properties

        public List<InfoAboutChat> InfoAboutAllChat { get; }
 

        #endregion Properties

        #region Constructors

        public NumbersOfChatsReceivedModelEventArgs(List<InfoAboutChat> infoAboutAllChat)
        {
            InfoAboutAllChat = infoAboutAllChat;
        }

        #endregion Constructors
    }
}
