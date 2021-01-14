using Common.Network;
using Common.Network.Packets;
using System;
using System.Collections.Generic;
using System.Text;

namespace Client.Model
{
    public class HandlerChats : IHandlerChats
    {
        private List<InfoAboutChat> _infoAboutAllChat;
        private ITransportClient _transportClient;
        private IHandlerConnection _handlerConnection;
        private IClientInfo _clientInfo;
        public event EventHandler<AddedChatEventArgs> AddedChat;
        public event EventHandler<AddedClientsToChatClientEvenArgs> AddedClientsToChat;
        public event EventHandler<RemovedClientsFromChatForVMEventArgs> RemovedClientsFromChat;
        public event EventHandler<RemovedChatEventArgs> RemovedChat;

        public HandlerChats(ITransportClient transportClient,IHandlerConnection handlerConnection, IHandlerResponseFromServer handlerResponseFromServer, IClientInfo clientInfo)
        {
            _handlerConnection = handlerConnection;
            _clientInfo = clientInfo;
            _transportClient = transportClient;
            handlerResponseFromServer.AddedChat += OnAddedChat;
            handlerResponseFromServer.AddedClientsToChat += OnAddedClientsToChat;
            handlerResponseFromServer.RemovedClientsFromChat += OnRemovedClientsFromChat;
            handlerResponseFromServer.ResponseNumbersChats += OnResponseNumbersChats;
            handlerResponseFromServer.ReceivedInfoAboutAllClients += OnReceivedInfoAboutAllClients;
            handlerResponseFromServer.RemovedChat += OnRemovedChat;
            _handlerConnection.AnotherClientConnected += ;
            _handlerConnection.AnotherNewClientConnected += ;
            _handlerConnection.AnotherClientDisconnected += ;
        }
        public void AddChat(List<string> namesOfClients)
        {
            namesOfClients.Insert(0, _clientInfo.Login);
            _transportClient.Send(Container.GetContainer(nameof(AddNewChatRequest),new AddNewChatRequest(_clientInfo.Login, namesOfClients)));
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
        private void OnReceivedInfoAboutAllClients(object sender, ReceivedInfoAboutAllClientsEventArgs container)
        {
            _transportClient.Send(Container.GetContainer(nameof(GetNumbersAccessibleChatsRequest),
                                                        new GetNumbersAccessibleChatsRequest(_clientInfo.Login)));
        }
        private void OnAddedChat(object sender, AddedNewChatModelEventArgs container)
        {
            Dictionary<string, bool> infoClientsForAdd = new Dictionary<string, bool>(_handlerConnection.InfoClientsAtChat);
            Dictionary<string, bool> infoClientsAtChat = new Dictionary<string, bool>();
            foreach(var item in container.Clients)
            {
                if(_handlerConnection.InfoClientsAtChat.TryGetValue(item,out bool activityClient))
                {
                    infoClientsAtChat.Add(item, activityClient);
                    infoClientsForAdd.Remove(item);
                }
            }
            AddedChat?.Invoke(this, new AddedChatEventArgs(container.ClientCreator, infoClientsAtChat, infoClientsForAdd, container.NumberChat));
        }
        private void OnRemovedChat(object sender, RemovedChatEventArgs container)
        {
            RemovedChat?.Invoke(this, new RemovedChatEventArgs(container.NameOfClient, container.NumberChat));
        }
        private void OnAddedClientsToChat(object sender, AddedClientsToChatEventArgs container)
        {
            Dictionary<string, bool> infoClientsAtChatForVM = new Dictionary<string, bool>();
            foreach (var item in container.Clients)
            {
                if (_handlerConnection.InfoClientsAtChat.TryGetValue(item, out bool activityClient))
                {
                    infoClientsAtChatForVM.Add(item, activityClient);
                }
            }
            AddedClientsToChat?.Invoke(this, new AddedClientsToChatClientEvenArgs(container.NumberChat, infoClientsAtChatForVM));
        }
        private void OnRemovedClientsFromChat(object sender, RemovedClientsFromChatEventArgs container)
        {
            Dictionary<string, bool> infoClientsAtChatForVM = new Dictionary<string, bool>();
            foreach (var item in container.Clients)
            {
                if (_handlerConnection.InfoClientsAtChat.TryGetValue(item, out bool activityClient))
                {
                    infoClientsAtChatForVM.Add(item, activityClient);
                }
            }
            RemovedClientsFromChat?.Invoke(this, new RemovedClientsFromChatForVMEventArgs(container.NameOfRemover, container.NumberChat, infoClientsAtChatForVM));
        }
        private void OnResponseNumbersChats(object sender, NumbersOfChatsReceivedEventArgs container)
        {
            if (container.InfoAboutAllChat.Count != 0)
            {
                _infoAboutAllChat = new List<InfoAboutChat>(container.InfoAboutAllChat);
                foreach (var item in _infoAboutAllChat)
                {
                    OnAddedChat(this, new AddedNewChatModelEventArgs(item.NameCreator, item.NumberChat, item.NamesOfClients));
                }
            }
            else
            {
                _transportClient.Send(Container.GetContainer(nameof(GetNumbersAccessibleChatsRequest),
                                                        new GetNumbersAccessibleChatsRequest(_clientInfo.Login)));
            }
        }
    }
}
