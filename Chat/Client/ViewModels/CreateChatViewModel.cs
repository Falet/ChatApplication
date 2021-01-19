namespace Client.ViewModels
{
    using Client.Model;
    using Prism.Commands;
    using Prism.Mvvm;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows;

    public class CreateChatViewModel : BindableBase
    {
        #region Fields

        private Visibility _visibilityView;
        private ObservableCollection<InfoAboutClient> _clientsCollection;
        private IHandlerChats _handlerChats;
        private IHandlerConnection _handlerConnection;

        #endregion Fields

        #region Properties

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

        #endregion Properties

        #region Constructors

        public CreateChatViewModel(IHandlerConnection handlerConnection, IHandlerChats handlerChats)
        {
            VisibilityCreateChat = Visibility.Hidden;
            _handlerChats = handlerChats;
            _handlerChats.AddedChat += OnAddedChat;
            _handlerConnection = handlerConnection;
            _handlerConnection.ReceivedInfoAboutAllClients += OnReceivedInfoAboutAllClients;
            _handlerConnection.AnotherClientConnected += OnAnotherClientConnected;
            _handlerConnection.AnotherNewClientConnected += OnAnotherNewClientConnected;
            _handlerConnection.AnotherClientDisconnected += OnAnotherClientDisconnected;
            _clientsCollection = new ObservableCollection<InfoAboutClient>();
            CreateChatButton = new DelegateCommand(CreateChat);
        }

        #endregion Constructors

        #region Methods

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
        private void OnAddedChat(object sender, AddedChatEventArgs container)
        {
            VisibilityCreateChat = Visibility.Hidden;
        }
        private void OnReceivedInfoAboutAllClients(object sender, ReceivedInfoAboutAllClientsEventArgs container)
        {
            App.Current.Dispatcher.Invoke(delegate
            {
                foreach (var KeyValue in container.InfoClientsAtChat)
                {
                    ClientsCollection.Add(new InfoAboutClient(KeyValue.Key, KeyValue.Value ? "Online" : "Offline"));
                }
            });
        }
        public void OnAnotherClientConnected(object sender, AnotherClientConnectedEventArgs container)
        {
            App.Current.Dispatcher.Invoke(delegate
            {
                foreach (var item in ClientsCollection.ToList())
                {
                    if (item.NameClient == container.NameClient)
                    {
                        item.ActivityClientChanged = "Online";
                        break;
                    }
                }
            });
        }
        public void OnAnotherNewClientConnected(object sender, AnotherClientConnectedEventArgs container)
        {
            App.Current.Dispatcher.Invoke(delegate
            {
                ClientsCollection.Add(new InfoAboutClient(container.NameClient, "Online"));
            });
        }
        public void OnAnotherClientDisconnected(object sender, AnotherClientDisconnectedEventArgs container)
        {
            App.Current.Dispatcher.Invoke(delegate
            {
                foreach (var item in ClientsCollection.ToList())
                {
                    if (item.NameClient == container.NameClient)
                    {
                        item.ActivityClientChanged = "Offline";
                        break;
                    }
                }
            });
        }

        #endregion Methods
    }
}
