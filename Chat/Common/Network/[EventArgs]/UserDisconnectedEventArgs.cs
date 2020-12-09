namespace Common.Network
{
    public class ClientDisconnectedEventArgs
    {
        #region Properties

        public string NameOfClient { get; }

        #endregion Properties

        #region Constructors

        public ClientDisconnectedEventArgs(string nameOfClient)
        {
            NameOfClient = nameOfClient;
        }

        #endregion Constructors
    }
}
