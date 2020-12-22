using Client.Model;
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

namespace Client.ViewModels
{
    public class ChatViewModel : BindableBase
    {
        private string _textMessages;
        private ControlVisibilityViewClientsViewModel _controlVisibilityViewClients;
        private string _textButtonChangeViewClients;
        private bool IsViewClientsChanged = true;
        private ObservableCollection<ListBoxItem> _messagesCollection;
        private string _textToolTip;
        private readonly IClientInfo _clientInfo;

        
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
        public ObservableCollection<ListBoxItem> MessagesCollection
        {
            get => _messagesCollection;
            set => SetProperty(ref _messagesCollection, value);
        }
        public string TextToolTip
        {
            get => _textToolTip;
            set => SetProperty(ref _textToolTip, value);
        }
        public int NameTab { get; set; }
        public DelegateCommand SendMessage { get; }
        public DelegateCommand ChangeVisibilityViewClients { get; }

        public ChatViewModel()
        {
            _clientInfo = new ClientInfo();
            
            _controlVisibilityViewClients = new ControlVisibilityViewClientsViewModel();
            _textButtonChangeViewClients = "Добавить";
            _messagesCollection = new ObservableCollection<ListBoxItem>();
            _textToolTip = "Добавить клиентов в чат из общего списка";

            SendMessage = new DelegateCommand(ExecuteSendMessage,IsMessageNotEmpty).ObservesProperty(() => CurrentTextMessage);
            ChangeVisibilityViewClients = new DelegateCommand(ChangeViewClients).ObservesProperty(() => TextButtonChangeViewClients);
            ChangeVisibilityViewClients.ObservesProperty(() => TextToolTip);
        }

        private void ExecuteSendMessage()
        {
            MessagesCollection.Add(new ListBoxItem()
            {
                Content = string.Format("Sender: {0}\nMessage: {1}", _clientInfo.Login, CurrentTextMessage)
            });
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
                _controlVisibilityViewClients.AddClientsAtChatViewModel.VisibilityOfControlAddClient = Visibility.Hidden;
                _controlVisibilityViewClients.ClientsAtChatViewModel.VisibilityClientsAtChat = Visibility.Visible;
                TextToolTip = "Добавить клиентов в чат из общего списка";
            }
            else
            {
                TextButtonChangeViewClients = "Назад";
                _controlVisibilityViewClients.ClientsAtChatViewModel.VisibilityClientsAtChat = Visibility.Hidden;
                _controlVisibilityViewClients.AddClientsAtChatViewModel.VisibilityOfControlAddClient = Visibility.Visible;
                TextToolTip = "Назад к списку клиентов в чате";
            }
        }
    }
}
