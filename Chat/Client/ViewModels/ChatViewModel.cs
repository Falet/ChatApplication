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
        private ObservableCollection<string> _messagesCollection;

        public string CurrentTextMessage
        {
            get => _textMessages;
            set => SetProperty(ref _textMessages, value);
        }
        public ControlVisibilityViewClientsViewModel controlVisibilityViewClients
        {
            get => _controlVisibilityViewClients;
            set => SetProperty(ref _controlVisibilityViewClients, value);
        }
        public ObservableCollection<string> MessagesCollection
        {
            get => _messagesCollection;
            set => SetProperty(ref _messagesCollection, value);
        }

        public DelegateCommand SendMessage { get; }

        public ChatViewModel()
        {
            _controlVisibilityViewClients = new ControlVisibilityViewClientsViewModel();
            _messagesCollection = new ObservableCollection<string>();

            SendMessage = new DelegateCommand(ExecuteSendMessage);
        }
        
        private void ExecuteSendMessage()
        {
            _messagesCollection.Add("asdfsdf");
        }
    }
}
