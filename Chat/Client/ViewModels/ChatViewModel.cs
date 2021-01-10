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
using System.Windows.Controls;
using System.Windows.Threading;

namespace Client.ViewModels
{
    public class ChatViewModel : BindableBase
    {
        private string _textMessages;
        private ControlVisibilityViewClientsViewModel _controlVisibilityViewClients;
        private AccessableClientForAddViewModel _allClientsViewModel;
        private ClientsAtChatViewModel _clientsAtChatViewModel;
        private string _textButtonChangeViewClients;
        private bool IsViewClientsChanged;
        private ObservableCollection<string> _messagesCollection;
        private string _textToolTip;
        private IHandlerMessages _handlerMessages;
        private Visibility _visibilityView;
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

        public ChatViewModel(AccessableClientForAddViewModel allClientsViewModel, ClientsAtChatViewModel clientsAtChatViewModel, IHandlerMessages handlerMessages, int numberChat)
        {

            _clientsAtChatViewModel = clientsAtChatViewModel;
            _allClientsViewModel = allClientsViewModel;
            _controlVisibilityViewClients = new ControlVisibilityViewClientsViewModel(_allClientsViewModel, clientsAtChatViewModel);
            _textButtonChangeViewClients = "Добавить";
            _messagesCollection = new ObservableCollection<string>();
            _textToolTip = "Добавить клиентов в чат из общего списка";
            IsViewClientsChanged = true;

            

            _handlerMessages = handlerMessages;
            _handlerMessages.MessageReceived += OnMessageReceived;
            _handlerMessages.ConnectedToChat += OnConnectedToChat;

            NumberChat = numberChat;

            SendMessage = new DelegateCommand(ExecuteSendMessage, IsMessageNotEmpty).ObservesProperty(() => CurrentTextMessage);
            ChangeVisibilityViewClients = new DelegateCommand(ChangeViewClients).ObservesProperty(() => TextButtonChangeViewClients);
            ChangeVisibilityViewClients.ObservesProperty(() => TextToolTip);
        }
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
        private void OnMessageReceived(object sender, MessageReceivedForVMEventArgs container)
        {
            if (container.NumberChat == NumberChat)
            {
                App.Current.Dispatcher.Invoke(delegate
                {
                    MessagesCollection.Add(string.Format("Sender: {0}\nMessage: {1}\nTime: {2}", container.Message.FromMessage, container.Message.Text, container.Message.Time));
                });
            }
        }
        private void OnConnectedToChat(object sender, ClientConnectedToChatEventArgs container)
        {
            if (container.AllMessageFromChat != null && container.NumberChat == NumberChat)
            {
                App.Current.Dispatcher.Invoke(delegate
                {
                    foreach (var item in container.AllMessageFromChat)
                    {
                        MessagesCollection.Add(string.Format("Sender: {0}\nMessage: {1}\nTime: {2}", item.FromMessage, item.Text, item.Time));
                    }
                });
            }
        }
    }
}
