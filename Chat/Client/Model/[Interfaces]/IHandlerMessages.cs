namespace Client.Model
{
    using Client.Model.Event;
    using System;

    public interface IHandlerMessages
    {

        #region Event

        event EventHandler<MessageReceivedVmEventArgs> MessageReceived;
        event EventHandler<ClientConnectedToChatVmEventArgs> ConnectedToChat;

        #endregion Event

        #region Methods

        void SendMessage(string message, int numberChat);
        void ConnectToChat(int numberChat);

        #endregion Methods
    }
}
