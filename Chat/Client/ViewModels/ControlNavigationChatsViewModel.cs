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
        private ObservableCollection<ControlTabChat> _chatCollection;

        private string _selectedTabChat;
        private string _textButtonChangeViewClients;
        private Dictionary<string, ChatViewModel> _allChats;
        public Visibility VisibilityNavigationChat
        {
            get => _visibilityView;
            set => SetProperty(ref _visibilityView, value);
        }
        public ObservableCollection<ControlTabChat> TabControlChat
        {
            get => _chatCollection;
            set => SetProperty(ref _chatCollection, value);
        }
        public string SelectedChat
        {
            get => _selectedTabChat;
            set => SetProperty(ref _selectedTabChat, value, () => ChangeViewModelOfViewChat());
        }
        public string TextButtonChangeViewClients
        {
            get => _textButtonChangeViewClients;
            set => SetProperty(ref _textButtonChangeViewClients, value);
        }

        public DelegateCommand CreateChat { get; }
        public DelegateCommand SelectChange { get; }
        public ControlNavigationChatsViewModel()
        {
            _chatCollection = new ObservableCollection<ControlTabChat>();

            _chatCollection.Add(new ControlTabChat() { NameTab = 1123 });
            _chatCollection.Add(new ControlTabChat() { NameTab = 1123 });

            ChatViewModel buf = new ChatViewModel();
            buf.NameTab = new Random().Next();

            CreateChat = new DelegateCommand(CreateChatView).ObservesProperty(() => TabControlChat);
        }
        private void ChangeViewModelOfViewChat()
        {
            TextButtonChangeViewClients = new Random().Next().ToString();
            if (_allChats.TryGetValue(SelectedChat, out ChatViewModel chat))
            {

            }
        }
        private void CreateChatView()
        {
            ChatViewModel buf = new ChatViewModel();
            buf.NameTab = new Random().Next();
        }
    }
}
