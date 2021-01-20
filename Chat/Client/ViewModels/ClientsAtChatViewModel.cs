using Client.Model;
namespace Client.ViewModels
{
    using Client.Model.Event;
    using Prism.Commands;
    using Prism.Mvvm;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows;

    public class ClientsAtChatViewModel : BindableBase
    {

        #region Fields

        private Visibility _visibilityViewClientsAtChat;
        private ObservableCollection<InfoAboutClientAtList> _collectionClientsAtChat;
        private IHandlerChats _handlerChats;
        private string _textError;
        private int _numberChat;

        #endregion Fields

        #region Properties

        public ObservableCollection<InfoAboutClientAtList> CollectionClientsAtChat
        {
            get => _collectionClientsAtChat;
            set => SetProperty(ref _collectionClientsAtChat, value);
        }
        public Visibility VisibilityClientsAtChat
        {
            get => _visibilityViewClientsAtChat;
            set => SetProperty(ref _visibilityViewClientsAtChat, value);
        }
        public string TextError
        {
            get => _textError;
            set => SetProperty(ref _textError, value);
        }
        public DelegateCommand RemoveButton { get; }

        #endregion Properties

        #region Constructors

        public ClientsAtChatViewModel(IHandlerConnection handlerConnection, IHandlerChats handlerChats, int numberChat, Dictionary<string, bool> clientForAdd)
        {
            _numberChat = numberChat;
            _collectionClientsAtChat = new ObservableCollection<InfoAboutClientAtList>();
            _handlerChats = handlerChats;
            _handlerChats.AddedClientsToChat += OnAddedClientsToChat;
            _handlerChats.RemovedClientsFromChat += OnRemovedClientsFromChat;
            handlerConnection.AnotherClientConnected += OnAnotherClientConnected;
            handlerConnection.AnotherClientDisconnected += OnAnotherClientDisconnected;

            AddClientsToCollection(clientForAdd);

            RemoveButton = new DelegateCommand(RemoveClientFromChat);
        }

        #endregion Constructors

        #region Methods

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
        public void OnAddedClientsToChat(object sender, AddedClientsToChatClientVmEvenArgs container)
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
                foreach (var KeyValue in clientForAdd)
                {
                    CollectionClientsAtChat.Add(new InfoAboutClientAtList(KeyValue.Key, KeyValue.Value ? "Online" : "Offline"));
                }
            });
        }
        private void OnRemovedClientsFromChat(object sender, RemovedClientsFromChatVmEventArgs container)
        {
            if (_numberChat == container.NumberChat)
            {
                App.Current.Dispatcher.Invoke(delegate
                {
                    foreach (var KeyValue in container.Clients)
                    {
                        foreach (var item in CollectionClientsAtChat.ToList())
                        {
                            if (item.NameClient == KeyValue.Key)
                            {
                                CollectionClientsAtChat.Remove(item);
                            }
                        }
                    }
                });
            }
        }
        public void OnAnotherClientConnected(object sender, AnotherClientConnectedVmEventArgs container)
        {
            App.Current.Dispatcher.Invoke(delegate
            {
                foreach (var item in CollectionClientsAtChat.ToList())
                {
                    if (item.NameClient == container.NameClient)
                    {
                        item.ActivityClientChanged = "Online";
                    }
                }
            });
        }
        public void OnAnotherClientDisconnected(object sender, AnotherClientDisconnectedVmEventArgs container)
        {
            App.Current.Dispatcher.Invoke(delegate
            {
                foreach (var item in CollectionClientsAtChat.ToList())
                {
                    if (item.NameClient == container.NameClient)
                    {
                        item.ActivityClientChanged = "Offline";
                    }
                }
            });
        }

        #endregion Methods

    }
}
