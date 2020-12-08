namespace Common.Network
{
    using System;
    public class ClientConnectedEventArgs
    {
        #region Properties

        public string ClientName { get; }

        public Guid ClientId { get; }

        #endregion Properties

        #region Constructors

        public ClientConnectedEventArgs(string clientName, Guid clientId)
        {
            ClientName = clientName;
            ClientId = clientId;
        }

        #endregion Constructors
    }
}