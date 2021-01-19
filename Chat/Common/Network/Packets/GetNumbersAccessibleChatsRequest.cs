using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Network.Packets
{
    public class GetNumbersAccessibleChatsRequest
    {
        #region Properties

        public string NameClient { get; }

        #endregion Properties

        #region Constructors

        public GetNumbersAccessibleChatsRequest(string nameClient)
        {
            NameClient = nameClient;
        }

        #endregion Constructors
    }
}
