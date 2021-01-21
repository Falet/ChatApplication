namespace Client.ViewModels
{
    using Client.Model;
    using Common.Network;
    using Prism.Commands;
    using Prism.Mvvm;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows;

    public class ControlNavigationChatsViewModel : BindableBase
    {
        #region Fields

        private Visibility _visibilityView = Visibility.Visible;
        private ObservableCollection<ChatViewModel> _chatCollection;
        private IHandlerMessages _handlerMessages;
        private IHandlerChats _handlerChats;
        private IHandlerConnection _handlerConnection;
        private ChatViewModel _selectedItemChat;
        private string _textButtonChangeViewClients;
        private ChatViewModel _currentViewModelChat;

        #endregion Fields

        #region Properties

        public Visibility VisibilityNavigationChat
        {
            get => _visibilityView;
            set => SetProperty(ref _visibilityView, value);
        }
        public ObservableCollection<ChatViewModel> ChatCollection
        {
            get => _chatCollection;
            set => SetProperty(ref _chatCollection, value);
        }
        public ChatViewModel SelectedChat
        {
            get => _selectedItemChat;
            set => SetProperty(ref _selectedItemChat, value, () => ChangeViewModelOfViewChat());
        }
        public string TextButtonChangeViewClients
        {
            get => _textButtonChangeViewClients;
            set => SetProperty(ref _textButtonChangeViewClients, value);
        }

        public ChatViewModel CurrentViewModelChat
        {
            get => _currentViewModelChat;
            set => SetProperty(ref _currentViewModelChat, value);
        }
        public DelegateCommand CreateChat { get; }
        public DelegateCommand SelectChange { get; }

        #endregion Properties

        #region Constructors

        public ControlNavigationChatsViewModel(IHandlerConnection handlerConnection, IHandlerMessages handlerMessages, IHandlerChats handlerChats)
        {
            _chatCollection = new ObservableCollection<ChatViewModel>();

            _handlerChats = handlerChats;
            _handlerChats.AddedChat += OnAddedChat;
            _handlerChats.RemovedChat += OnRemovedChat;
            _handlerConnection = handlerConnection;
            _handlerMessages = handlerMessages;
        }

        #endregion Constructors

        #region Methods

        private void ChangeViewModelOfViewChat()
        {
            if (CurrentViewModelChat != null)
            {
                CurrentViewModelChat.VisibilityChat = Visibility.Hidden;
            }
            CurrentViewModelChat = SelectedChat;
            CurrentViewModelChat.VisibilityChat = Visibility.Visible;
            if (!CurrentViewModelChat.ChatIsLoad)
            {
                _handlerMessages.ConnectToChat(CurrentViewModelChat.NumberChat);
            }
        }
        private void OnAddedChat(object sender, AddedChatVmEventArgs container)
        {
            App.Current.Dispatcher.Invoke(delegate
            {
                AccessableClientForAddViewModel allClientViewModel = new AccessableClientForAddViewModel(_handlerConnection, _handlerChats,
                                                                                       container.AccessNameClientForAdd, container.NumberChat);
                ClientsAtChatViewModel clientsAtChat = new ClientsAtChatViewModel(_handlerConnection, _handlerChats,
                                                                                  container.NumberChat, container.NameOfClientsForAdd);
                ChatViewModel newChat = new ChatViewModel(allClientViewModel, clientsAtChat, _handlerMessages, container.NumberChat);

                ChatCollection.Add(newChat);

                CurrentViewModelChat = newChat;
            });
        }
        private void OnRemovedChat(object sender, RemovedChatEventArgs container)
        {
            App.Current.Dispatcher.Invoke(delegate
            {
                foreach (var item in ChatCollection.ToList())
                {
                    if (item.NumberChat == container.NumberChat)
                    {
                        ChatCollection.Remove(item);
                        SelectedChat = ChatCollection.Last();
                    }
                }
            });
        }

        #endregion Methods
    }
}
