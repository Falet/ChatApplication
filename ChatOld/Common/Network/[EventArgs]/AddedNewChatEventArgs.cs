namespace Common.Network
{
    using System.Collections.Generic;
    public class AddedNewChatEventArgs
    {
        #region Properties

        public string NameOfClientSender { get; }

        public List<string> NameOfClientsForAdd { get; }

        #endregion Properties

        #region Constructors 

        public AddedNewChatEventArgs(string nameofClientSender, List<string> nameOfClients)
        {
            NameOfClientSender = nameofClientSender;
            NameOfClientsForAdd = nameOfClients;
        }

        #endregion Constructors
    }
}
