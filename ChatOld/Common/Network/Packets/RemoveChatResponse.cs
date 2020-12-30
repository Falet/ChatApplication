namespace Common.Network.Packets
{
    public class RemoveChatResponse
    {
        #region Properties

        public int NumberChat { get; }

        #endregion Properties


        #region Constructors

        public RemoveChatResponse(int numberChat)
        {
            NumberChat = numberChat;
        }

        #endregion Constructors
    }
}
