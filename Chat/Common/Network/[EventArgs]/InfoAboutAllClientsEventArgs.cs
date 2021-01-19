namespace Common.Network
{
    public class InfoAboutAllClientsEventArgs
    {
        #region Properties

        public string NameClient { get; }

        #endregion Properties

        #region Constructors

        public InfoAboutAllClientsEventArgs(string nameClient)
        {
            NameClient = nameClient;
        }

        #endregion Constructors
    }
}
