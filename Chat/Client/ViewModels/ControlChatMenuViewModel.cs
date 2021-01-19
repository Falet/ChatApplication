namespace Client.ViewModels
{
    using Client.Model;
    using Common.Network;
    using Prism.Commands;
    using Prism.Mvvm;
    using System.Windows;

    public class ControlChatMenuViewModel : BindableBase
    {
        #region Fields

        private Visibility _visibilityView;
        private ControlVisibilityElementsOfChatMenuViewModel _controlVisibilityElements;
        private IHandlerConnection _handlerConnection;
        private string _textButtonChangeViewClients;
        private string _textToolTip;
        private bool IsViewClientsChanged;

        #endregion Fields

        #region Properties

        public Visibility VisibilityChatMenu
        {
            get => _visibilityView;
            set => SetProperty(ref _visibilityView, value);
        }
        public string TextButtonChangeViewChatMenu
        {
            get => _textButtonChangeViewClients;
            set => SetProperty(ref _textButtonChangeViewClients, value);
        }
        public ControlVisibilityElementsOfChatMenuViewModel ControlVisibilityElements
        {
            get => _controlVisibilityElements;
            set => SetProperty(ref _controlVisibilityElements, value);
        }
        public string TextToolTip
        {
            get => _textToolTip;
            set => SetProperty(ref _textToolTip, value);
        }
        public DelegateCommand CreateChat { get; }

        #endregion Properties

        #region Constructors

        public ControlChatMenuViewModel(IHandlerConnection handlerConnection, IHandlerMessages handlerMessages, IHandlerChats handlerChats)
        {
            _visibilityView = Visibility.Hidden;

            _handlerConnection = handlerConnection;
            _handlerConnection.ClientConnected += OnClientConnected;

            handlerChats.AddedChat += OnAddedChat;

            _controlVisibilityElements = new ControlVisibilityElementsOfChatMenuViewModel(handlerConnection, handlerMessages, handlerChats);

            _textButtonChangeViewClients = "Создать";
            _textToolTip = "Создать чат";
            IsViewClientsChanged = false;

            CreateChat = new DelegateCommand(ChangeViewClients).ObservesProperty(() => TextButtonChangeViewChatMenu);
            CreateChat.ObservesProperty(() => TextToolTip);
        }

        #endregion Constructors

        #region Methods

        private void OnAddedChat(object sender, AddedChatEventArgs container)
        {
            TextButtonChangeViewChatMenu = "Создать";
            ControlVisibilityElements.ControlNavigationChatsViewModel.VisibilityNavigationChat = Visibility.Visible;
            ControlVisibilityElements.CreateChat.VisibilityCreateChat = Visibility.Hidden;
            TextToolTip = "Создать чат";
        }
        private void ChangeViewClients()
        {
            IsViewClientsChanged = !IsViewClientsChanged;
            if (IsViewClientsChanged)
            {
                TextButtonChangeViewChatMenu = "Назад";
                ControlVisibilityElements.ControlNavigationChatsViewModel.VisibilityNavigationChat = Visibility.Hidden;
                ControlVisibilityElements.CreateChat.VisibilityCreateChat = Visibility.Visible;
                TextToolTip = "Назад к списку чатов";
            }
            else
            {
                TextButtonChangeViewChatMenu = "Создать";
                ControlVisibilityElements.ControlNavigationChatsViewModel.VisibilityNavigationChat = Visibility.Visible;
                ControlVisibilityElements.CreateChat.VisibilityCreateChat = Visibility.Hidden;
                TextToolTip = "Создать чат";
            }
        }
        private void OnClientConnected(object sender, ClientConnectedToServerEventArgs container)
        {
            if (container.Result == ResultRequest.Ok)
            {
                VisibilityChatMenu = Visibility.Visible;
            }
        }


        #endregion Methods
    }
}
