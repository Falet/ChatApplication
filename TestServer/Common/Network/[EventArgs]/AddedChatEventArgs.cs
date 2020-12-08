namespace Common.Network
{
    using System.Collections.Generic;
    public class AddedChatEventArgs
    {
        #region Properties

        public string NameOfClientSender { get; }

        public List<string> NameOfClientsForAdd { get; }

        #endregion Properties

        #region Constructors

        public AddedChatEventArgs(string nameofClientSender, List<string> nameOfClients)
        {
            NameOfClientSender = nameofClientSender;
            NameOfClientsForAdd = nameOfClients;
        }

        #endregion Constructors
    }
}
