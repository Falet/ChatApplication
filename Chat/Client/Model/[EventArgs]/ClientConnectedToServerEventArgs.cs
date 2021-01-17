namespace Client.Model
{
    using Common.Network;

    public class ClientConnectedToServerEventArgs
    {
        #region Properties

        public ResultRequest Result { get; }

        public string Reason { get; }

        #endregion Properties

        #region Constructors

        public ClientConnectedToServerEventArgs(ResultRequest result, string reason)
        {
            Result = result;
            Reason = reason;
        }

        #endregion Constructors
    }
}
