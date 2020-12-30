namespace Common.Network.Packets
{
    public class ConnectionRequest
    {
        #region Properties

        public string ClientName { get; }

        #endregion Properties

        #region Constructors

        public ConnectionRequest(string clientname)
        {
            ClientName = clientname;
        }

        #endregion Constructors
    }
}
