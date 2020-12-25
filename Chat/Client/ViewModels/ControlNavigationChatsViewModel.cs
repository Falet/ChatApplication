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

            AddClientsAtChatViewModel addClientsToChat = new AddClientsAtChatViewModel(_handlerConnection, _handlerChats);
        }
        private void ChangeViewModelOfViewChat()
        {
            CurrentViewModelChat = _selectedItemChat;
            _handlerMessages.Send(CurrentViewModelChat.NumberChat);
        }
        private void OnCreateChat(object sender, AddedChatEventArgs container)
        {
            ClientsAtChatViewModel clientsAtChat = new ClientsAtChatViewModel(_handlerConnection, _handlerChats,
                                                                              container.NumberChat, container.NameOfClientsForAdd);
            ChatViewModel newChat = new ChatViewModel(addClientsToChat, clientsAtChat, _handlerMessages, container.NumberChat);
            SelectedChat = newChat;
            _chatCollection.Add(newChat);
        }
    }
}
