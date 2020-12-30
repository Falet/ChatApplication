namespace Common.Network.Packets
{
    using System.Collections.Generic;
    public class GetNumbersAccessibleChatsResponse
    {
        #region Properties

        public Dictionary<LinkNumberChatCreator, List<string>> AllInfoAboutChat { get; }


        #endregion Properties

        #region Constructors

        public GetNumbersAccessibleChatsResponse(Dictionary<LinkNumberChatCreator, List<string>> allInfoAboutChat)
        {
            AllInfoAboutChat = allInfoAboutChat;
        }

        #endregion Constructors
    }
}
