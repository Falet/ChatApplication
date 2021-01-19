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
using System.Windows.Threading;

namespace Client.ViewModels
{
    public class CreateChatViewModel : BindableBase
    {
        private Visibility _visibilityView;
        private ObservableCollection<InfoAboutClient> _clientsCollection;
        private IHandlerChats _handlerChats;
        private IHandlerConnection _handlerConnection;
        public Visibility VisibilityCreateChat
        {
            get => _visibilityView;
            set => SetProperty(ref _visibilityView, value);
        }
        public ObservableCollection<InfoAboutClient> ClientsCollection
        {
            get => _clientsCollection;
            set => SetProperty(ref _clientsCollection, value);
        }
        public DelegateCommand CreateChatButton { get; }
        public CreateChatViewModel(IHandlerConnection handlerConnection, IHandlerChats handlerChats)
        {
            VisibilityCreateChat = Visibility.Hidden;
            _handlerChats = handlerChats;
            _handlerChats.AddedChat += OnCreatedChat;
            _handlerConnection = handlerConnection;
            _handlerConnection.ReceivedInfoAboutAllClients += OnReceivedInfoAboutAllClients;
            _handlerConnection.AnotherClientConnected += OnConnectAnotherClient;
            _handlerConnection.AnotherNewClientConnected += OnConnectAnotherNewClient;
            _handlerConnection.AnotherClientDisconnected += OnDisconnectAnotherClient;
            _clientsCollection = new ObservableCollection<InfoAboutClient>();
            CreateChatButton = new DelegateCommand(CreateChat);
        }
        private void CreateChat()
        {
            List<string> ClientForAdd = new List<string>();
            foreach (var item in ClientsCollection.ToList())
            {
                if (item.IsSelectedClient)
                {
                    ClientForAdd.Add(item.NameClient);
                    item.IsSelectedClient = false;
                }
            }
            _handlerChats.AddChat(ClientForAdd);
        }
        private void OnCreatedChat(object sender, AddedChatEventArgs container)
        {
            VisibilityCreateChat = Visibility.Hidden;
        }
        private void OnReceivedInfoAboutAllClients(object sender, ReceivedInfoAboutAllClientsEventArgs container)
        {
            App.Current.Dispatcher.Invoke(delegate
            {
                foreach (var KeyValue in container.InfoClientsAtChat)
                {
                    ClientsCollection.Add(new InfoAboutClient(KeyValue.Key, KeyValue.Value));
                }
            });
        }
        public void OnConnectAnotherClient(object sender, AnotherClientConnectedEventArgs container)
        {
            App.Current.Dispatcher.Invoke(delegate
            {
                foreach (var item in ClientsCollection.ToList())
                {
                    if (item.NameClient == container.NameClient)
                    {
                        item.ActivityClient = true;
                    }
                }
            });
        }
        public void OnConnectAnotherNewClient(object sender, AnotherClientConnectedEventArgs container)
        {
            App.Current.Dispatcher.Invoke(delegate
            {
                ClientsCollection.Add(new InfoAboutClient(container.NameClient, true));
            });
        }
        public void OnDisconnectAnotherClient(object sender, AnotherClientDisconnectedEventArgs container)
        {
            App.Current.Dispatcher.Invoke(delegate
            {
                foreach (var item in ClientsCollection.ToList())
                {
                    if (item.NameClient == container.NameClient)
                    {
                        item.ActivityClient = false;
                    }
                }
            });
        }
    }
}
