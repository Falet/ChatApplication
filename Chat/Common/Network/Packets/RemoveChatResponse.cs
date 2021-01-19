namespace Common.Network.Packets
{
    public class RemoveChatResponse
    {
        #region Properties

        public string NameOfClient { get; }
        public int NumberChat { get; }

        #endregion Properties


        #region Constructors

        public RemoveChatResponse(string nameOfClient, int numberChat)
        {
            NameOfClient = nameOfClient;
            NumberChat = numberChat;
        }

        #endregion Constructors
    }
}
