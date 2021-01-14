using Client.Model;
using Common.Network;
using Prism.Commands;
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
        private IHandlerChats _handlerChats;
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
        public DelegateCommand RemoveButton { get; }
        public ClientsAtChatViewModel(IHandlerConnection handlerConnection, IHandlerChats handlerChats, int numberChat, Dictionary<string, bool> clientForAdd)
        {
            _numberChat = numberChat;
            _collectionClientsAtChat = new ObservableCollection<InfoAboutClient>();
            _handlerChats = handlerChats;
            _handlerChats.AddedClientsToChat += OnClientAddedToChat;
            _handlerChats.RemovedClientsFromChat += OnClientRemovedFromChat;
            handlerConnection.AnotherClientConnected += OnConnectedAnotherClient;
            handlerConnection.AnotherClientDisconnected += OnDisconnectedClient;
            AddClientsToCollection(clientForAdd);
            RemoveButton = new DelegateCommand(RemoveClientFromChat);
        }
        private void RemoveClientFromChat()
        {
            List<string> ClientForRemove = new List<string>();
            foreach (var item in CollectionClientsAtChat.ToList())
            {
                if (item.IsSelectedClient)
                {
                    ClientForRemove.Add(item.NameClient);
                }
            }
            _handlerChats.RemoveClientFromChat(_numberChat, ClientForRemove);
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
            App.Current.Dispatcher.Invoke(delegate
            {
                foreach(var KeyValue in clientForAdd)
                {
                    CollectionClientsAtChat.Add(new InfoAboutClient(KeyValue.Key, KeyValue.Value));
                }
            });
        }
        private void OnClientRemovedFromChat(object sender, RemovedClientsFromChatForVMEventArgs container)
        {
            if(_numberChat == container.NumberChat)
            {
                App.Current.Dispatcher.Invoke(delegate
                {
                    foreach (var KeyValue in container.Clients)
                    {
                        foreach(var item in CollectionClientsAtChat.ToList())
                        {
                            if(item.NameClient == KeyValue.Key)
                            {
                                CollectionClientsAtChat.Remove(item);
                            }
                        }
                    }
                });
            }
        }
        public void OnConnectedAnotherClient(object sender, AnotherClientConnectedEventArgs container)
        {
            App.Current.Dispatcher.Invoke(delegate
            {
                foreach (var item in CollectionClientsAtChat.ToList())
                {
                    if (item.NameClient == container.NameClient)
                    {
                        item.ActivityClientChanged = true;
                    }
                }
            });
        }
        public void OnDisconnectedClient(object sender, AnotherClientDisconnectedEventArgs container)
        {
            App.Current.Dispatcher.Invoke(delegate
            {
                foreach (var item in CollectionClientsAtChat.ToList())
                {
                    if (item.NameClient == container.NameClient)
                    {
                        item.ActivityClientChanged = false;
                    }
                }
            });
        }
    }
}
