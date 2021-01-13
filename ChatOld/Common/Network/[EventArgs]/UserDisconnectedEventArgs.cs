namespace Common.Network
{
    public class ClientDisconnectedEventArgs
    {
        #region Properties

        public string NameClient { get; }

        #endregion Properties

        #region Constructors

        public ClientDisconnectedEventArgs(string nameOfClient)
        {
            NameClient = nameOfClient;
        }

        #endregion Constructors
    }
}
