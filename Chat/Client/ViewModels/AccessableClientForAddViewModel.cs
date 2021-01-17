namespace Client.ViewModels
{
    using Client.Model;
    using Prism.Commands;
    using Prism.Mvvm;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows;

    public class AccessableClientForAddViewModel : BindableBase
    {
        #region Fields

        private Visibility _visibilityViewAllClient;
        private ObservableCollection<InfoAboutClient> _clientsCollection;
        private IHandlerChats _handlerChats;
        private int _numberChat;
        private string _textError;

        #endregion Fields

        #region Properties

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
        public string TextError
        {
            get => _textError;
            set => SetProperty(ref _textError, value);
        }
        public DelegateCommand AddClientToChatButton { get; }

        #endregion Properties


        #region Constructors

        public AccessableClientForAddViewModel(IHandlerConnection handlerConnection, IHandlerChats handlerChats, Dictionary<string, bool> accessNameClientForAdd, int numberChat)
        {
            _numberChat = numberChat;

            _clientsCollection = new ObservableCollection<InfoAboutClient>();
            _handlerChats = handlerChats;
            _handlerChats.AddedClientsToChat += OnAddedClientsToChat;
            _handlerChats.RemovedClientsFromChat += OnRemovedClientFormChat;
            handlerConnection.AnotherClientConnected += OnAnotherClientConnected;
            handlerConnection.AnotherNewClientConnected += OnAnotherNewClientConnected;
            handlerConnection.AnotherClientDisconnected += OnAnotherClientDisconnected;

            SetCollection(accessNameClientForAdd);

            AddClientToChatButton = new DelegateCommand(AddClientToChat);
        }

        #endregion Constructors

        #region Methods

        private void SetCollection(Dictionary<string, bool> accessNameClientForAdd)
        {
            App.Current.Dispatcher.Invoke(delegate
            {
                foreach (var KeyValue in accessNameClientForAdd)
                {
                    ClientsAccessableCollection.Add(new InfoAboutClient(KeyValue.Key, KeyValue.Value ? "Online" : "Offline"));
                }
            });
        }
        private void OnAnotherClientConnected(object sender, AnotherClientConnectedEventArgs container)
        {
            App.Current.Dispatcher.Invoke(delegate
            {
                foreach (var item in ClientsAccessableCollection.ToList())
                {
                    if (item.NameClient == container.NameClient)
                    {
                        item.ActivityClientChanged = "Online";
                    }
                }
            });
        }
        private void OnAnotherNewClientConnected(object sender, AnotherClientConnectedEventArgs container)
        {
            App.Current.Dispatcher.Invoke(delegate
            {
                ClientsAccessableCollection.Add(new InfoAboutClient(container.NameClient, "Online"));
            });
        }
        private void OnAnotherClientDisconnected(object sender, AnotherClientDisconnectedEventArgs container)
        {
            App.Current.Dispatcher.Invoke(delegate
            {
                foreach (var item in ClientsAccessableCollection.ToList())
                {
                    if (item.NameClient == container.NameClient)
                    {
                        item.ActivityClientChanged = "Offline";
                    }
                }
            });
        }
        private void OnRemovedClientFormChat(object sender, RemovedClientsFromChatForVMEventArgs container)
        {
            if (container.NumberChat == _numberChat)
            {
                App.Current.Dispatcher.Invoke(delegate
                {
                    SetCollection(container.Clients);
                });
            }
        }
        private void OnAddedClientsToChat(object sender, AddedClientsToChatClientEvenArgs container)
        {
            if (container.NumberChat == _numberChat)
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

        #endregion Methods
    }
}
