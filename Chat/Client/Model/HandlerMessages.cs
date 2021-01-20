namespace Client.Model
{
    using Client.Model.Event;
    using Common.Network.Packets;
    using System;

    public class HandlerMessages : IHandlerMessages
    {
        #region Fields

        private ITransportClient _transportClient;
        private IClientInfo _clientInfo;

        #endregion Fields

        #region Event

        public event EventHandler<MessageReceivedVmEventArgs> MessageReceived;
        public event EventHandler<ClientConnectedToChatVmEventArgs> ConnectedToChat;

        #endregion Event

        #region Constructors

        public HandlerMessages(ITransportClient transportClient, IHandlerResponseFromServer handlerResponseFromServer, IClientInfo clientInfo)
        {
            _transportClient = transportClient;
            _clientInfo = clientInfo;
            handlerResponseFromServer.MessageReceived += OnMessageReceived;
            handlerResponseFromServer.ConnectedToChat += OnConnectedToChat;
        }

        #endregion Constructors

        #region Methods

        public void ConnectToChat(int numberChat)
        {
            _transportClient.Send(Container.GetContainer(nameof(ConnectToChatRequest), new ConnectToChatRequest(_clientInfo.Login, numberChat)));
        }
        public void SendMessage(string message, int numberChat)
        {
            _transportClient.Send(Container.GetContainer(nameof(MessageRequest), new MessageRequest(_clientInfo.Login, message, numberChat)));
        }
        private void OnMessageReceived(object sender, MessageReceivedVmEventArgs container)
        {
            MessageReceived?.Invoke(this, new MessageReceivedVmEventArgs(container.Message, container.NumberChat));
        }
        private void OnConnectedToChat(object sender, ClientConnectedToChatVmEventArgs container)
        {
            ConnectedToChat?.Invoke(this, new ClientConnectedToChatVmEventArgs(container.AllMessageFromChat, container.NumberChat));
        }

        #endregion Methods
    }
}
