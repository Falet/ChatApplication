namespace Common.Network.Packets
{
    using System.Collections.Generic;
    public class NumbersAccessibleChatsResponse
    {
        #region Properties

        public List<InfoAboutChat> AllInfoAboutChat { get; }

        #endregion Properties

        #region Constructors

        public NumbersAccessibleChatsResponse(List<InfoAboutChat> allInfoAboutChat)
        {
            AllInfoAboutChat = allInfoAboutChat;
        }

        #endregion Constructors
    }
}
