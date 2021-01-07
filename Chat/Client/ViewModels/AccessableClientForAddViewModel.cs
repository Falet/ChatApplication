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
            _handlerChats = handlerChats;
            _clientsCollection = new ObservableCollection<InfoAboutClient>();
            handlerConnection.AnotherClientConnected += OnConnectAnotherClient;
            handlerConnection.AnotherClientDisconnected += OnDisconnectClient;
            SetStartCollection(accessNameClientForAdd);
            AddClientToChatButton = new DelegateCommand(AddClientToChat);
        }
        private void SetStartCollection(Dictionary<string, bool> accessNameClientForAdd)
        {
            foreach (var KeyValue in accessNameClientForAdd)
            {
                ClientsAccessableCollection.Add(new InfoAboutClient(KeyValue.Key, KeyValue.Value));
            }
        }
        public void OnConnectAnotherClient(object sender, AnotherClientConnectedEventArgs container)
        {
            foreach (var item in ClientsAccessableCollection.ToList())
            {
                if(item.NameClient == container.NameClient)
                {
                    item.ActivityClient = true;
                }
                else
                {
                    ClientsAccessableCollection.Add(new InfoAboutClient(container.NameClient, true));
                }
            }
        }
        public void OnDisconnectClient(object sender, ClientDisconnectedEventArgs container)
        {
            foreach (var item in ClientsAccessableCollection)
            {
                if (item.NameClient == container.NameClient)
                {
                    item.ActivityClient = false;
                }
            }
        }
        public void AddClientToChat()
        {
            List<string> ClientForAdd = new List<string>();
            foreach(var item in ClientsAccessableCollection.ToList())
            {
                if(item.IsSelectedClient)
                {
                    ClientForAdd.Add(item.NameClient);
                    item.IsSelectedClient = false;
                }
            }
            _handlerChats.AddClientToChat(_numberChat, ClientForAdd);
        }
    }
}
