namespace Client.Model
{
    using Common.Network;
    using System.Collections.Generic;

    public class NumbersOfChatsReceivedEventArgs
    {
        #region Properties

        public List<InfoAboutChat> InfoAboutAllChat { get; }
 

        #endregion Properties

        #region Constructors

        public NumbersOfChatsReceivedEventArgs(List<InfoAboutChat> infoAboutAllChat)
        {
            InfoAboutAllChat = infoAboutAllChat;
        }

        #endregion Constructors
    }
}
