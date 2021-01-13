namespace Common.Network.Packets
{
    using System.Collections.Generic;
    public class AddNewChatRequest
    {
        #region Properties
        public string NameClientSender { get; }
        public List<string> Clients { get; }

        #endregion Properties


        #region Constructors

        public AddNewChatRequest(string nameClientSender,List<string> clients)
        {
            NameClientSender = nameClientSender;
            Clients = clients;
        }

        #endregion Constructors
    }
}
