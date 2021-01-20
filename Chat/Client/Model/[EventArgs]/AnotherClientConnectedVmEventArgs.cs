namespace Client.Model.Event
{
    public class AnotherClientConnectedVmEventArgs
    {
        #region Properties

        public string NameClient { get; }

        #endregion Properties

        #region Constructors

        public AnotherClientConnectedVmEventArgs(string nameClient)
        {
            NameClient = nameClient;
        }

        #endregion Constructors

    }
}
