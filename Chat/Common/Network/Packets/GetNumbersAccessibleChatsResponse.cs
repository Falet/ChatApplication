namespace Common.Network.Packets
{
    using System.Collections.Generic;
    public class GetNumbersAccessibleChatsResponse
    {
        #region Properties

        public Dictionary<LinkNumberChatCreator, ClientsAtChat> AllInfoAboutChat { get; }


        #endregion Properties

        #region Constructors

        public GetNumbersAccessibleChatsResponse(Dictionary<LinkNumberChatCreator, ClientsAtChat> allInfoAboutChat)
        {
            AllInfoAboutChat = allInfoAboutChat;
        }

        #endregion Constructors
    }
}
