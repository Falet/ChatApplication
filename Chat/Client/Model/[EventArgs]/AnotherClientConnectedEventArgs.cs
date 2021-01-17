namespace Client.Model
{
    public class AnotherClientConnectedEventArgs
    {
        #region Properties

        public string NameClient { get; }

        #endregion Properties

        #region Constructors

        public AnotherClientConnectedEventArgs(string nameClient)
        {
            NameClient = nameClient;
        }

        #endregion Constructors

    }
}
