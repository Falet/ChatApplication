namespace Common.Network.Packets
{
    using System.Collections.Generic;
    public class AddNewChatRequest
    {
        #region Properties

        public List<string> Clients { get; }

        #endregion Properties


        #region Constructors

        public AddNewChatRequest(List<string> clients)
        {
            Clients = clients;
        }

        #endregion Constructors
    }
}
