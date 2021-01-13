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

        public ClientDisconnectedEventArgs(Guid nameGuid, string nameClient)
        {
            NameGuid = nameGuid;
            NameClient = nameClient;
        }

        #endregion Constructors
    }
}
