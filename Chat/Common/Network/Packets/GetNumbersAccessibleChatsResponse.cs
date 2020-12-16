namespace Common.Network.Packets
{
    using System.Collections.Generic;
    public class GetNumbersAccessibleChatsResponse
    {
        #region Properties

        public Dictionary<int, string> NumbersChatsForClient { get; }
        public Dictionary<string, bool> InfoClientsAtChat { get; }
        
        #endregion Properties

        #region Constructors

        public GetNumbersAccessibleChatsResponse(Dictionary<string, bool> infoClientsAtChat, Dictionary<int, string> numbersChatsForClient)
        {
            InfoClientsAtChat = infoClientsAtChat;
            NumbersChatsForClient = numbersChatsForClient;
        }

        #endregion Constructors
    }
}
