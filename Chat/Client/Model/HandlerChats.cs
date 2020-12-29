using Common.Network;
using Common.Network.Packets;
using System;
using System.Collections.Generic;
using System.Text;

namespace Client.Model
{
    public class HandlerChats : IHandlerChats
    {
        private ITransportClient _transportClient;
        private ClientInfo _clientInfo;
        public event EventHandler<AddedChatEventArgs> AddedChat;
        public event EventHandler<AddedClientsToChatClientEvenArgs> AddedClientsToChat;
        public event EventHandler<RemovedClientsFromChatEventArgs> RemovedClientsFromChat;
        public event EventHandler<NumbersOfChatsReceivedEventArgs> ResponseNumbersChats;

        public HandlerChats(ITransportClient transportClient, IHandlerResponseFromServer handlerResponseFromServer, ClientInfo clientInfo)
        {
            _clientInfo = clientInfo;
            _transportClient = transportClient;
        }
        public void AddChat(List<string> namesOfClients)
        {
            namesOfClients.Insert(0, _clientInfo.Login);
            _transportClient.Send(Container.GetContainer(nameof(AddNewChatRequest),new AddNewChatRequest(namesOfClients)));
        }

        public void AddClientToChat(int numberChat, List<string> namesOfClients)
        {
            _transportClient.Send(Container.GetContainer(nameof(AddNewClientToChatRequest),
                                                         new AddNewClientToChatRequest(_clientInfo.Login, namesOfClients, numberChat)));
        }

        public void RemoveClientFromChat(int numberChat, List<string> namesOfClients)
        {
            _transportClient.Send(Container.GetContainer(nameof(RemoveClientFromChatRequest),
                                                         new RemoveClientFromChatRequest(_clientInfo.Login, namesOfClients, numberChat)));
        }

        private void RequestNumbersChat()
        {
            _transportClient.Send(Container.GetContainer(nameof(GetNumbersAccessibleChatsRequest),
                                                         new GetNumbersAccessibleChatsRequest(_clientInfo.Login)));
        }

    }
}
