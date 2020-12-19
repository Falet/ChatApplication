using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Client.ViewModels
{
    public class ControlVisibilityViewClientsViewModel : BindableBase
    {
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

        public ControlVisibilityViewClientsViewModel()
        {
            AddClientsAtChatViewModel = new AddClientsAtChatViewModel();
            ClientsAtChatViewModel = new ClientsAtChatViewModel();
            ClientsAtChatViewModel.VisibilityClientsAtChat = Visibility.Visible;
            AddClientsAtChatViewModel.VisibilityOfControlAddClient = Visibility.Hidden;
        }
    }
}
