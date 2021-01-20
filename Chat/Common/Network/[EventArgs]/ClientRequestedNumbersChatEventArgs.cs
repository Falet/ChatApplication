namespace Common.Network
{
    public class ClientRequestedNumbersChatEventArgs
    {
        #region Properties

        public string NameOfClientSender { get; }

        #endregion Properties

        #region Constructors

        public ClientRequestedNumbersChatEventArgs(string nameofClientSender)
        {
            NameOfClientSender = nameofClientSender;
        }

        #endregion Constructors
    }
}
