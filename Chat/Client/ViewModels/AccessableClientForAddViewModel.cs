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
    public class AccessableClientForAddViewModel : BindableBase
    {
        private Visibility _visibilityViewAllClient;
        private ObservableCollection<InfoAboutClient> _clientsCollection;
        private IHandlerChats _handlerChats;
        private int _numberChat;
        public Visibility VisibilityOfControlAllClient
        {
            get => _visibilityViewAllClient;
            set => SetProperty(ref _visibilityViewAllClient, value);
        }
        public ObservableCollection<InfoAboutClient> ClientsAccessableCollection
        {
            get => _clientsCollection;
            set => SetProperty(ref _clientsCollection, value);
        }
        public DelegateCommand AddClientToChatButton { get; }
        public AccessableClientForAddViewModel(IHandlerConnection handlerConnection, IHandlerChats handlerChats, Dictionary<string, bool> accessNameClientForAdd, int numberChat)
        {
            _numberChat = numberChat;
            
            _clientsCollection = new ObservableCollection<InfoAboutClient>();
            _handlerChats = handlerChats;
            _handlerChats.AddedClientsToChat += OnAddedClientsToChat;
            _handlerChats.RemovedClientsFromChat += OnRemovedClientFormChat;
            handlerConnection.AnotherClientConnected += OnConnectAnotherClient;
            handlerConnection.AnotherNewClientConnected += OnAnotherNewClientConnected;
            handlerConnection.AnotherClientDisconnected += OnDisconnectClient;
            SetStartCollection(accessNameClientForAdd);
            AddClientToChatButton = new DelegateCommand(AddClientToChat);
        }
        private void SetStartCollection(Dictionary<string, bool> accessNameClientForAdd)
        {
            App.Current.Dispatcher.Invoke(delegate
            {
                foreach (var KeyValue in accessNameClientForAdd)
                {
                    ClientsAccessableCollection.Add(new InfoAboutClient(KeyValue.Key, KeyValue.Value));
                }
            });
        }
        private void OnConnectAnotherClient(object sender, AnotherClientConnectedEventArgs container)
        {
            App.Current.Dispatcher.Invoke(delegate
            {
                foreach (var item in ClientsAccessableCollection.ToList())
                {
                    if (item.NameClient == container.NameClient)
                    {
                        item.ActivityClientChanged = true;
                    }
                }
            });
        }
        private void OnAnotherNewClientConnected(object sender, AnotherClientConnectedEventArgs container)
        {
            App.Current.Dispatcher.Invoke(delegate
            {
                 ClientsAccessableCollection.Add(new InfoAboutClient(container.NameClient, true));
            });
        }
        private void OnDisconnectClient(object sender, AnotherClientDisconnectedEventArgs container)
        {
            App.Current.Dispatcher.Invoke(delegate
            {
                foreach (var item in ClientsAccessableCollection.ToList())
                {
                    if (item.NameClient == container.NameClient)
                    {
                        item.ActivityClientChanged = false;
                    }
                }
            });
        }
        private void OnRemovedClientFormChat(object sender, RemovedClientsFromChatForVMEventArgs container)
        {
            if(container.NumberChat == _numberChat)
            {
                App.Current.Dispatcher.Invoke(delegate
                {
                    foreach (var KeyValue in container.Clients)
                    {
                        ClientsAccessableCollection.Add(new InfoAboutClient(KeyValue.Key, KeyValue.Value));
                    }
                });
            }
        }
        private void OnAddedClientsToChat(object sender, AddedClientsToChatClientEvenArgs container)
        {
            if(container.NumberChat == _numberChat)
            {
                App.Current.Dispatcher.Invoke(delegate
                {
                    foreach (var KeyValue in container.NameOfClients)
                    {
                        foreach (var item in ClientsAccessableCollection.ToList())
                        {
                            if (item.NameClient == KeyValue.Key)
                            {
                                ClientsAccessableCollection.Remove(item);
                            }
                        }
                    }
                });
            }
        }
        private void AddClientToChat()
        {
            App.Current.Dispatcher.Invoke(delegate
            {
                List<string> ClientForAdd = new List<string>();
                foreach (var item in ClientsAccessableCollection.ToList())
                {
                    if (item.IsSelectedClient)
                    {
                        ClientForAdd.Add(item.NameClient);
                        item.IsSelectedClient = false;
                    }
                }
                _handlerChats.AddClientToChat(_numberChat, ClientForAdd);
            });
        }

    }
}
