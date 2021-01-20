namespace Client.Model.Event
{
    public class AnotherClientDisconnectedVmEventArgs
    {
        #region Properties

        public string NameClient { get; }

        #endregion Properties

        #region Constructors

        public AnotherClientDisconnectedVmEventArgs(string nameClient)
        {
            NameClient = nameClient;
        }

        #endregion Constructors
    }
}
