namespace Common.Network.Packets
{
    using System.Collections.Generic;
    public class GetNumbersAccessibleChatsResponse
    {
        #region Properties

        public Dictionary<int, string> NumbersChatsForClient { get; }
        public List<string> ClientsAtChat { get; }
        
        #endregion Properties

        #region Constructors

        public GetNumbersAccessibleChatsResponse( Dictionary<int, string> numbersChatsForClient, List<string> clientsAtChat)
        {
            ClientsAtChat = clientsAtChat;
            NumbersChatsForClient = numbersChatsForClient;
        }

        #endregion Constructors
    }
}
