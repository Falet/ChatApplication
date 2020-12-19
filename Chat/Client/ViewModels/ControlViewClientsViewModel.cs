using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Client.ViewModels
{
    public class ControlViewClientsViewModel : BindableBase
    {
        private Visibility _visibilityViewClientsAtChat = Visibility.Visible;
        public AddClientsAtChatViewModel AddClientsAtChatViewModel
        {
            get;
            private set;
        }
        public ClientsAtChatViewModel ClientsAtChatViewModel
        {
            get;
            private set;
        }
        public Visibility VisibilityClientsAtChat
        {
            get => _visibilityViewClientsAtChat;
            set => SetProperty(ref _visibilityViewClientsAtChat, value);
        }

        private Visibility _visibilityViewAddClients = Visibility.Hidden;
        public Visibility VisibilityOfControlAddClient
        {
            get => _visibilityViewAddClients;
            set => SetProperty(ref _visibilityViewAddClients, value);
        }
        public ControlViewClientsViewModel()
        {
            AddClientsAtChatViewModel = new AddClientsAtChatViewModel();
            ClientsAtChatViewModel = new ClientsAtChatViewModel();
        }
    }
}
