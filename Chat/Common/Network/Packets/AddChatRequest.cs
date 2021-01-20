namespace Common.Network.Packets
{
    using System.Collections.Generic;
    public class AddChatRequest
    {
        #region Properties
        public string NameClientSender { get; }
        public List<string> Clients { get; }

        #endregion Properties


        #region Constructors

        public AddChatRequest(string nameClientSender,List<string> clients)
        {
            NameClientSender = nameClientSender;
            Clients = clients;
        }

        #endregion Constructors
    }
}
