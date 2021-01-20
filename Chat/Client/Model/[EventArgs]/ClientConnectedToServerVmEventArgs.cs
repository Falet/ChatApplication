namespace Client.Model.Event
{
    using Common.Network;

    public class ClientConnectedToServerVmEventArgs
    {
        #region Properties

        public ResultRequest Result { get; }

        public string Reason { get; }

        #endregion Properties

        #region Constructors

        public ClientConnectedToServerVmEventArgs(ResultRequest result, string reason)
        {
            Result = result;
            Reason = reason;
        }

        #endregion Constructors
    }
}
