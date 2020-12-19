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
        private List<string> _messagesCollection;
        private string _textMessages;
        private ControlViewClientsViewModel _controlViewClients;

        public List<string> MessagesCollection
        {
            get => _messagesCollection;
            set => SetProperty(ref _messagesCollection, value);
        }
        
        public string CurrentTextMessage
        {
            get => _textMessages;
            set => SetProperty(ref _textMessages, value);
        }
        public ControlViewClientsViewModel controlViewClients
        {
            get => _controlViewClients;
            set => SetProperty(ref _controlViewClients, value);
        }
        

        public DelegateCommand SendMessage { get; }

        public ChatViewModel()
        {
            _controlViewClients = new ControlViewClientsViewModel();
            _messagesCollection = new List<string>();
            _messagesCollection.Add("adsf");
            SendMessage = new DelegateCommand(ExecuteSendMessage);
        }
        
        private void ExecuteSendMessage()
        {

        }
    }
}
