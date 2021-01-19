namespace Client.Model
{
    using Common.Network.Packets;

    public interface ITransportClient
    {
        #region Methods

        void Connect(string ip, int port);
        void Send(MessageContainer message);

        #endregion Methods
    }
}
