using System;

namespace Common.Network
{
    public class ClientDisconnectedEventArgs
    {
        #region Properties

        public Guid NameGuid { get; }

        public string NameClient { get; }

        #endregion Properties

        #region Constructors

        public ClientDisconnectedEventArgs(string nameClient,Guid nameGuid)
        {
            NameGuid = nameGuid;
            NameClient = nameClient;
        }

        #endregion Constructors
    }
}
