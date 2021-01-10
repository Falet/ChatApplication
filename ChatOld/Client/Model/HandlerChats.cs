﻿using Common.Network;
using Common.Network.Packets;
using System;
using System.Collections.Generic;
using System.Text;

namespace Client.Model
{
    public class HandlerChats : IHandlerChats
    {
        private ITransportClient _transportClient;
        private IHandlerConnection _handlerConnection;
        private ClientInfo _clientInfo;
        public event EventHandler<AddedChatEventArgs> AddedChat;
        public event EventHandler<AddedClientsToChatClientEvenArgs> AddedClientsToChat;
        public event EventHandler<RemovedClientsFromChatForVMEventArgs> RemovedClientsFromChat;
        public event EventHandler<NumbersOfChatsReceivedEventArgs> ResponseNumbersChats;

        public HandlerChats(ITransportClient transportClient,IHandlerConnection handlerConnection, IHandlerResponseFromServer handlerResponseFromServer, ClientInfo clientInfo)
        {
            _handlerConnection = handlerConnection;
            _clientInfo = clientInfo;
            _transportClient = transportClient;
            handlerResponseFromServer.AddedChat += OnAddedChat;
            handlerResponseFromServer.AddedClientsToChat += OnAddedClientsToChat;
            handlerResponseFromServer.RemovedClientsFromChat += OnRemovedClientsFromChat;
            handlerResponseFromServer.ResponseNumbersChats += OnResponseNumbersChats;
            handlerResponseFromServer.ReceivedInfoAboutAllClients += OnReceivedInfoAboutAllClients;
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
        private void OnReceivedInfoAboutAllClients(object sender, ReceivedInfoAboutAllClientsEventArgs container)
        {
            _transportClient.Send(Container.GetContainer(nameof(GetNumbersAccessibleChatsRequest),
                                                        new GetNumbersAccessibleChatsRequest(_clientInfo.Login)));
        }
        private void OnAddedChat(object sender, AddedNewChatModelEventArgs container)
        {
            Dictionary<string, bool> infoClientsForAdd = _handlerConnection.InfoClientsAtChat;
            Dictionary<string, bool> infoClientsAtChatForVM = new Dictionary<string, bool>();
            foreach(var item in container.Clients)
            {
                if(_handlerConnection.InfoClientsAtChat.TryGetValue(item,out bool activityClient))
                {
                    infoClientsAtChatForVM.Add(item, activityClient);
                    infoClientsForAdd.Remove(item);
                }
            }
            AddedChat?.Invoke(this, new AddedChatEventArgs(container.ClientCreator, infoClientsAtChatForVM, infoClientsForAdd, container.NumberChat));
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
            if(_handlerConnection.InfoClientsAtChat.Count != 0)
            {
                foreach (var item in container.AllInfoAboutChat)
                {
                    OnAddedChat(this, new AddedNewChatModelEventArgs(item.Key.NameCreator, item.Key.NumberChat, item.Value));
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