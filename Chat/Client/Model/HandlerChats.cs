namespace Client.Model
{
    using Client.Model.Event;
    using Common.Network;
    using Common.Network.Packets;
    using System;
    using System.Collections.Generic;

    public class HandlerChats : IHandlerChats
    {
        #region Fields

        private List<InfoAboutChat> _infoAboutAllChat;
        private ITransportClient _transportClient;
        private IHandlerConnection _handlerConnection;
        private IClientInfo _clientInfo;

        #endregion Fields

        #region Event

        public event EventHandler<AddedChatVmEventArgs> AddedChat;
        public event EventHandler<RemovedChatEventArgs> RemovedChat;
        public event EventHandler<AddedClientsToChatClientVmEvenArgs> AddedClientsToChat;
        public event EventHandler<RemovedClientsFromChatVmEventArgs> RemovedClientsFromChat;

        #endregion Event

        #region Constructors

        public HandlerChats(ITransportClient transportClient, IHandlerConnection handlerConnection, IHandlerResponseFromServer handlerResponseFromServer, IClientInfo clientInfo)
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
        }

        #endregion Constructors

        #region Methods

        public void AddChat(List<string> namesOfClients)
        {
            namesOfClients.Insert(0, _clientInfo.Login);
            _transportClient.Send(Container.GetContainer(nameof(AddChatRequest), new AddChatRequest(_clientInfo.Login, namesOfClients)));
        }

        public void AddClientToChat(int numberChat, List<string> namesOfClients)
        {
            _transportClient.Send(Container.GetContainer(nameof(AddClientToChatRequest),
                                                         new AddClientToChatRequest(_clientInfo.Login, namesOfClients, numberChat)));
        }

        public void RemoveClientFromChat(int numberChat, List<string> namesOfClients)
        {
            _transportClient.Send(Container.GetContainer(nameof(RemoveClientFromChatRequest),
                                                         new RemoveClientFromChatRequest(_clientInfo.Login, namesOfClients, numberChat)));
        }
        private void OnReceivedInfoAboutAllClients(object sender, ReceivedInfoAboutAllClientsVmEventArgs container)
        {
            _transportClient.Send(Container.GetContainer(nameof(NumbersAccessibleChatsRequest),
                                                        new NumbersAccessibleChatsRequest(_clientInfo.Login)));
        }
        private void OnAddedChat(object sender, AddedNewChatModelEventArgs container)
        {
            CreateChat(container.ClientCreator, container.NumberChat, container.Clients);
        }
        private void OnRemovedChat(object sender, RemovedChatEventArgs container)
        {
            RemovedChat?.Invoke(this, new RemovedChatEventArgs(container.NameClient, container.NumberChat));
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
            AddedClientsToChat?.Invoke(this, new AddedClientsToChatClientVmEvenArgs(container.NumberChat, infoClientsAtChatForVM));
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
            RemovedClientsFromChat?.Invoke(this, new RemovedClientsFromChatVmEventArgs(container.NameOfRemover, container.NumberChat, infoClientsAtChatForVM));
        }
        private void OnResponseNumbersChats(object sender, NumbersOfChatsReceivedModelEventArgs container)
        {
            if (container.InfoAboutAllChat.Count != 0)
            {
                _infoAboutAllChat = new List<InfoAboutChat>(container.InfoAboutAllChat);
                foreach (var item in _infoAboutAllChat)
                {
                    CreateChat(item.NameCreator, item.NumberChat, item.NamesOfClients);
                }
            }
            else
            {
                _transportClient.Send(Container.GetContainer(nameof(NumbersAccessibleChatsRequest),
                                                        new NumbersAccessibleChatsRequest(_clientInfo.Login)));
            }
        }
        private void CreateChat(string clientCreator, int numberChat, List<string> clients)
        {
            Dictionary<string, bool> infoClientsForAdd = new Dictionary<string, bool>(_handlerConnection.InfoClientsAtChat);
            Dictionary<string, bool> infoClientsAtChat = new Dictionary<string, bool>();
            foreach (var item in clients)
            {
                if (_handlerConnection.InfoClientsAtChat.TryGetValue(item, out bool activityClient))
                {
                    infoClientsAtChat.Add(item, activityClient);
                    infoClientsForAdd.Remove(item);
                }
            }
            AddedChat?.Invoke(this, new AddedChatVmEventArgs(clientCreator, infoClientsAtChat, infoClientsForAdd, numberChat));
        }
        #endregion Methods
    }
}
