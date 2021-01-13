using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Network.Packets
{
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
