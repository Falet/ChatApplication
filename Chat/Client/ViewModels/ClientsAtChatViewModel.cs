using Client.Model;
using Common.Network;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Client.ViewModels
{
    public class ClientsAtChatViewModel : BindableBase
    {
        private Visibility _visibilityViewClientsAtChat;
        private ObservableCollection<InfoAboutClient> _collectionClientsAtChat;
        private int _numberChat;
        public ObservableCollection<InfoAboutClient> CollectionClientsAtChat
        {
            get => _collectionClientsAtChat;
            set => SetProperty(ref _collectionClientsAtChat, value);
        }
        public Visibility VisibilityClientsAtChat
        {
            get => _visibilityViewClientsAtChat;
            set => SetProperty(ref _visibilityViewClientsAtChat, value);
        }
        public ClientsAtChatViewModel(IHandlerConnection handlerConnection, IHandlerChats handlerChats, int numberChat, Dictionary<string, bool> clientForAdd)
        {
            _numberChat = numberChat;
            _collectionClientsAtChat = new ObservableCollection<InfoAboutClient>();
            handlerChats.AddedClientsToChat += OnClientAddedToChat;
            handlerConnection.AnotherClientConnected += OnConnectAnotherClient;
            handlerConnection.ClientDisconnected += OnDisconnectClient;
            AddClientsToCollection(clientForAdd);
        }
        public void OnClientAddedToChat(object sender, AddedClientsToChatClientEvenArgs container)
        {
            if (container.NumberChat == _numberChat)
            {
                AddClientsToCollection(container.NameOfClients);
            }
        }
        private void AddClientsToCollection(Dictionary<string, bool> clientForAdd)
        {
            foreach (var KeyValue in clientForAdd)
            {
                CollectionClientsAtChat.Add(new InfoAboutClient(KeyValue.Key, KeyValue.Value));
            }
        }
        public void OnConnectAnotherClient(object sender, AnotherClientConnectedEventArgs container)
        {
            foreach (var item in CollectionClientsAtChat)
            {
                if (item.NameClient == container.NameClient)
                {
                    item.ActivityClient = true;
                }
            }
        }
        public void OnDisconnectClient(object sender, ClientDisconnectedEventArgs container)
        {
            foreach (var item in CollectionClientsAtChat)
            {
                if (item.NameClient == container.NameOfClient)
                {
                    item.ActivityClient = false;
                }
            }
        }
    }
}
