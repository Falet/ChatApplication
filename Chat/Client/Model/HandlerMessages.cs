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
        private ClientInfo _clientInfo;

        public event EventHandler<MessageReceivedEventArgs> MessageReceived;
        public event EventHandler<ConnectionToChatEventArgs> ConnectedToChat;
        public HandlerMessages(ITransportClient transportClient, IHandlerResponseFromServer handlerResponseFromServer, ClientInfo clientInfo)
        {
            _transportClient = transportClient;
            _clientInfo = clientInfo;
        }
        public void ConnectToChat(int numberChat)
        {
            _transportClient.Send(Container.GetContainer(nameof(ConnectToChatRequest),new ConnectToChatRequest(_clientInfo.Login, numberChat)));
        }

        public void SendMessage(string message, int numberChat)
        {
            _transportClient.Send(Container.GetContainer(nameof(MessageRequest),new MessageRequest(_clientInfo.Login, message, numberChat)));
        }
    }
}
