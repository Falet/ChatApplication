namespace Common.Network.Packets
{
    public class NumbersAccessibleChatsRequest
    {
        #region Properties

        public string NameClient { get; }

        #endregion Properties

        #region Constructors

        public NumbersAccessibleChatsRequest(string nameClient)
        {
            NameClient = nameClient;
        }

        #endregion Constructors
    }
}
