using Common.Network;
using Common.Network.Packets;
using System;
using System.Collections.Generic;
using System.Text;

namespace Client.Model
{
    public class HandlerMessages : IHandlerMessages
    {
        private ITransportClient _transportClient;
        private IClientInfo _clientInfo;

        public event EventHandler<MessageReceivedForVMEventArgs> MessageReceived;
        public event EventHandler<ClientConnectedToChatEventArgs> ConnectedToChat;
        public HandlerMessages(ITransportClient transportClient, IHandlerResponseFromServer handlerResponseFromServer, IClientInfo clientInfo)
        {
            _transportClient = transportClient;
            _clientInfo = clientInfo;
            handlerResponseFromServer.MessageReceived += OnMessageReceived;
            handlerResponseFromServer.ConnectedToChat += OnConnectedToChat;
        }
        public void ConnectToChat(int numberChat)
        {
            _transportClient.Send(Container.GetContainer(nameof(ConnectToChatRequest),new ConnectToChatRequest(_clientInfo.Login, numberChat)));
        }
        public void SendMessage(string message, int numberChat)
        {
            _transportClient.Send(Container.GetContainer(nameof(MessageRequest),new MessageRequest(_clientInfo.Login, message, numberChat)));
        }
        private void OnMessageReceived(object sender, MessageReceivedForVMEventArgs container)
        {
            MessageReceived?.Invoke(this, new MessageReceivedForVMEventArgs(container.Message, container.NumberChat));
        }
        private void OnConnectedToChat(object sender, ClientConnectedToChatEventArgs container)
        {
            ConnectedToChat?.Invoke(this, new ClientConnectedToChatEventArgs(container.AllMessageFromChat, container.NumberChat));
        }
    }
}
