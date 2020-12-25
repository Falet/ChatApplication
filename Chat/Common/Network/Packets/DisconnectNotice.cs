using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Network.Packets
{
    public class DisconnectNotice
    {
        #region Properties

        public string NameOfClient { get; }

        public List<int> NumberChats { get; }

        #endregion Properties

        #region Constructors

        public DisconnectNotice(string nameOfClient,List<int> numberChats)
        {
            NameOfClient = nameOfClient;
            NumberChats = numberChats;
        }

        #endregion Constructors
    }
}
