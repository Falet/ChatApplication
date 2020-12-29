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
    public class ControlNavigationChatsViewModel : BindableBase
    {
        private Visibility _visibilityView = Visibility.Visible;
        private ObservableCollection<ChatViewModel> _chatCollection;
        private IHandlerMessages _handlerMessages;
        private IHandlerChats _handlerChats;
        private IHandlerConnection _handlerConnection;
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
        public ControlNavigationChatsViewModel(IHandlerConnection handlerConnection, IHandlerMessages handlerMessages, IHandlerChats handlerChats)
        {
            _chatCollection = new ObservableCollection<ChatViewModel>();

            _handlerChats = handlerChats;
            _handlerChats.AddedChat += OnCreateChat;
            _handlerConnection = handlerConnection;
            _handlerMessages = handlerMessages;

            AccessClientForAddViewModel allClientViewModel = new AccessClientForAddViewModel(_handlerConnection, _handlerChats, 
                                                                                             new Dictionary<string, bool>(), 123);
            ClientsAtChatViewModel clientsAtChat = new ClientsAtChatViewModel(_handlerConnection, _handlerChats,
                                                                              123, new Dictionary<string, bool>());
            ChatViewModel newChat = new ChatViewModel(allClientViewModel, clientsAtChat, _handlerMessages, 123);
            ChatCollection.Add(newChat);
            SelectedChat = newChat;
        }
        private void ChangeViewModelOfViewChat()
        {
            CurrentViewModelChat = _selectedItemChat;
            _handlerMessages.ConnectToChat(CurrentViewModelChat.NumberChat);
        }
        private void OnCreateChat(object sender, AddedChatEventArgs container)
        {
            AccessClientForAddViewModel allClientViewModel = new AccessClientForAddViewModel(_handlerConnection, _handlerChats, 
                                                                                       container.AccessNameClientForAdd, container.NumberChat);
            ClientsAtChatViewModel clientsAtChat = new ClientsAtChatViewModel(_handlerConnection, _handlerChats,
                                                                              container.NumberChat, container.NameOfClientsForAdd);
            ChatViewModel newChat = new ChatViewModel(allClientViewModel, clientsAtChat, _handlerMessages, container.NumberChat);
            ChatCollection.Add(newChat);
            SelectedChat = newChat;
        }
    }
}
