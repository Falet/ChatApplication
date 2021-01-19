using Common.Network;
using System;
using System.Collections.Generic;
using System.Text;

namespace Client.Model
{
    public class ClientConnectedToServerEventArgs
    {
        #region Properties

        public ResultRequest Result { get; }

        public string Reason { get; }

        #endregion Properties

        #region Constructors

        public ClientConnectedToServerEventArgs(ResultRequest result, string reason)
        {
            Result = result;
            Reason = reason;
        }

        #endregion Constructors
    }
}
