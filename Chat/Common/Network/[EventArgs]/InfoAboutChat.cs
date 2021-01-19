namespace Common.Network
{
    using System.Collections.Generic;

    public class InfoAboutChat
    {
        #region Properties

        public int NumberChat { get; }
        public string NameCreator { get; }
        public List<string> NamesOfClients { get; }

        #endregion Properties

        #region Constructors

        public InfoAboutChat(int numberChat, string nameCreator, List<string> namesOfClients)
        {
            NamesOfClients = namesOfClients;
            NumberChat = numberChat;
            NameCreator = nameCreator;
        }

        #endregion Constructors
    }
}
