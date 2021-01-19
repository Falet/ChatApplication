using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Network.Packets
{
    public class DisconnectNotice
    {
        #region Properties

        public string NameOfClient { get; }


        #endregion Properties

        #region Constructors

        public DisconnectNotice(string nameOfClient)
        {
            NameOfClient = nameOfClient;
        }

        #endregion Constructors
    }
}
