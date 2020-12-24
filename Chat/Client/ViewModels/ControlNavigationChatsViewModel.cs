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

namespace Client.ViewModels
{
    public class ControlNavigationChatsViewModel : BindableBase
    {
        private Visibility _visibilityView = Visibility.Visible;
        private ObservableCollection<ChatViewModel> _chatCollection;
        private IHandlerMessages _handlerMessages;
        private IHandlerChats _handlerChats;
        private ChatViewModel _selectedItemChat;
        private string _textButtonChangeViewClients;
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
        private ChatViewModel _currentViewModelChat;
        public ChatViewModel CurrentViewModelChat
        {
            get => _currentViewModelChat;
            set => SetProperty(ref _currentViewModelChat, value);
        }
        public DelegateCommand CreateChat { get; }
        public DelegateCommand SelectChange { get; }
        public ControlNavigationChatsViewModel(IHandlerMessages handlerMessages, IHandlerChats handlerChats)
        {
            _chatCollection = new ObservableCollection<ChatViewModel>();

            CreateChatView();

            _handlerMessages = handlerMessages;
            _handlerChats = handlerChats;

            CreateChat = new DelegateCommand(CreateChatView).ObservesProperty(() => ChatCollection);
        }
        private void ChangeViewModelOfViewChat()
        {
            CurrentViewModelChat = _selectedItemChat;
        }
        private void CreateChatView()
        {
            int numberChat = new Random().Next();
            ChatViewModel newChat = new ChatViewModel(_handlerMessages, numberChat);
            SelectedChat = newChat;
            _chatCollection.Add(newChat);
        }
    }
}
