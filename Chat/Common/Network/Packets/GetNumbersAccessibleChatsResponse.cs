namespace Common.Network.Packets
{
    using System.Collections.Generic;
    public class GetNumbersAccessibleChatsResponse
    {
        #region Properties

        public List<InfoAboutChat> AllInfoAboutChat { get; }

        #endregion Properties

        #region Constructors

        public GetNumbersAccessibleChatsResponse(List<InfoAboutChat> allInfoAboutChat)
        {
            AllInfoAboutChat = allInfoAboutChat;
        }

        #endregion Constructors
    }
}
