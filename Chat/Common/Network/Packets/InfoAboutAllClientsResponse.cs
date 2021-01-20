namespace Common.Network.Packets
{
    using System.Collections.Generic;

    public class InfoAboutAllClientsResponse
    {
        #region Properties

        public Dictionary<string, bool> InfoAboutAllClients { get; }

        #endregion Properties

        #region Constructors

        public InfoAboutAllClientsResponse(Dictionary<string, bool> infoAboutAllClients)
        {
            InfoAboutAllClients = infoAboutAllClients;
        }

        #endregion Constructors
    }
}
