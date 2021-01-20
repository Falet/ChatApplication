namespace Common.Network.Packets
{
    public class InfoAboutAllClientsRequest
    {
        #region Properties

        public string NameClient { get; }

        #endregion Properties

        #region Constructors

        public InfoAboutAllClientsRequest(string nameClient)
        {
            NameClient = nameClient;
        }

        #endregion Constructors
    }
}
