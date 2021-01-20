namespace Common.Network.Packets
{
    public class RemoveChatResponse
    {
        #region Properties

        public string NameClient { get; }
        public int NumberChat { get; }

        #endregion Properties


        #region Constructors

        public RemoveChatResponse(string nameClient, int numberChat)
        {
            NameClient = nameClient;
            NumberChat = numberChat;
        }

        #endregion Constructors
    }
}
