namespace Server.DataBase
{
    public class GeneralChatInfo
    {
        #region Properties

        public int NumberGeneralChat { get; }
        public string TypeGeneralChat { get; }
        public string OwnerGeneralChat { get; }

        #endregion Properties

        #region Constructors

        public GeneralChatInfo(int numberGeneralChat, string typeGeneralChat, string ownerGeneralChat)
        {
            NumberGeneralChat = numberGeneralChat;
            TypeGeneralChat = typeGeneralChat;
            OwnerGeneralChat = ownerGeneralChat;
        }

        #endregion Constructors
    }
}
