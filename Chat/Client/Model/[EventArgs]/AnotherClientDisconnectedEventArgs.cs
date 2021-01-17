namespace Client.Model
{
    public class AnotherClientDisconnectedEventArgs
    {
        #region Properties

        public string NameClient { get; }

        #endregion Properties

        #region Constructors

        public AnotherClientDisconnectedEventArgs(string nameClient)
        {
            NameClient = nameClient;
        }

        #endregion Constructors
    }
}
