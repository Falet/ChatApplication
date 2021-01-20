namespace Client.ViewModels
{
    using Client.Model;
    using Client.Model.Event;
    using Prism.Commands;
    using Prism.Mvvm;
    using System.Collections.ObjectModel;
    using System.Windows;

    public class ChatViewModel : BindableBase
    {
        #region Fields

        private Visibility _visibilityView;
        private ControlVisibilityViewClientsViewModel _controlVisibilityViewClients;
        private string _textButtonChangeViewClients;
        private string _textMessages;
        private string _textToolTip;
        private bool IsViewClientsChanged;
        private ObservableCollection<string> _messagesCollection;
        private IHandlerMessages _handlerMessages;

        #endregion Fields

        #region Properties

        public bool ChatIsLoad { get; private set; }
        public Visibility VisibilityChat
        {
            get => _visibilityView;
            set => SetProperty(ref _visibilityView, value);
        }
        public string CurrentTextMessage
        {
            get => _textMessages;
            set => SetProperty(ref _textMessages, value);
        }
        public ControlVisibilityViewClientsViewModel ControlVisibilityViewClients
        {
            get => _controlVisibilityViewClients;
            set => SetProperty(ref _controlVisibilityViewClients, value);
        }
        public string TextButtonChangeViewClients
        {
            get => _textButtonChangeViewClients;
            set => SetProperty(ref _textButtonChangeViewClients, value);
        }
        public ObservableCollection<string> MessagesCollection
        {
            get => _messagesCollection;
            set => SetProperty(ref _messagesCollection, value);
        }
        public string TextToolTip
        {
            get => _textToolTip;
            set => SetProperty(ref _textToolTip, value);
        }
        public int NumberChat { get; }
        public DelegateCommand SendMessage { get; }
        public DelegateCommand ChangeVisibilityViewClients { get; }

        #endregion Properties

        #region Constructors

        public ChatViewModel(AccessableClientForAddViewModel allClientsViewModel, ClientsAtChatViewModel clientsAtChatViewModel, IHandlerMessages handlerMessages, int numberChat)
        {
            _visibilityView = Visibility.Hidden;

            _controlVisibilityViewClients = new ControlVisibilityViewClientsViewModel(allClientsViewModel, clientsAtChatViewModel);
            _textButtonChangeViewClients = "Добавить";
            _messagesCollection = new ObservableCollection<string>();
            _textToolTip = "Добавить клиентов в чат из общего списка";
            IsViewClientsChanged = true;

            ChatIsLoad = false;

            _handlerMessages = handlerMessages;
            _handlerMessages.MessageReceived += OnMessageReceived;
            _handlerMessages.ConnectedToChat += OnConnectedToChat;

            NumberChat = numberChat;

            SendMessage = new DelegateCommand(ExecuteSendMessage, IsMessageNotEmpty).ObservesProperty(() => CurrentTextMessage);
            ChangeVisibilityViewClients = new DelegateCommand(ChangeViewClients).ObservesProperty(() => TextButtonChangeViewClients);
            ChangeVisibilityViewClients.ObservesProperty(() => TextToolTip);
        }

        #endregion Constructors

        #region Methods

        private void ExecuteSendMessage()
        {
            _handlerMessages.SendMessage(CurrentTextMessage, NumberChat);
            CurrentTextMessage = null;
        }
        private bool IsMessageNotEmpty()
        {
            return !string.IsNullOrWhiteSpace(CurrentTextMessage);
        }
        private void ChangeViewClients()
        {
            IsViewClientsChanged = !IsViewClientsChanged;
            if (IsViewClientsChanged)
            {
                TextButtonChangeViewClients = "Добавить";
                ControlVisibilityViewClients.AccessClientForAddViewModel.VisibilityOfControlAllClient = Visibility.Hidden;
                ControlVisibilityViewClients.ClientsAtChatViewModel.VisibilityClientsAtChat = Visibility.Visible;
                TextToolTip = "Добавить клиентов в чат из общего списка";
            }
            else
            {
                TextButtonChangeViewClients = "Назад";
                ControlVisibilityViewClients.ClientsAtChatViewModel.VisibilityClientsAtChat = Visibility.Hidden;
                ControlVisibilityViewClients.AccessClientForAddViewModel.VisibilityOfControlAllClient = Visibility.Visible;
                TextToolTip = "Назад к списку клиентов в чате";
            }
        }
        private void OnMessageReceived(object sender, MessageReceivedVmEventArgs container)
        {
            if (container.NumberChat == NumberChat)
            {
                App.Current.Dispatcher.Invoke(delegate
                {
                    MessagesCollection.Add(string.Format("Sender: {0}\nMessage: {1}\nTime: {2}", container.Message.FromMessage, container.Message.Text, container.Message.Time));
                });
            }
        }
        private void OnConnectedToChat(object sender, ClientConnectedToChatVmEventArgs container)
        {
            if (container.NumberChat == NumberChat)
            {
                App.Current.Dispatcher.Invoke(delegate
                {
                    foreach (var item in container.AllMessageFromChat)
                    {
                        MessagesCollection.Add(string.Format("Sender: {0}\nMessage: {1}\nTime: {2}", item.FromMessage, item.Text, item.Time));
                    }
                    ChatIsLoad = true;
                });
            }
        }

        #endregion Methods
    }
}
