namespace Client.Model
{
    using System;

    public interface IHandlerMessages
    {

        #region Event

        event EventHandler<MessageReceivedForVMEventArgs> MessageReceived;
        event EventHandler<ClientConnectedToChatEventArgs> ConnectedToChat;

        #endregion Event

        #region Methods

        void SendMessage(string message, int numberChat);
        void ConnectToChat(int numberChat);

        #endregion Methods
    }
}
