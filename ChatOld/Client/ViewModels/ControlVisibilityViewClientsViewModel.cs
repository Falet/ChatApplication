using Client.Model;
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
        public AccessClientForAddViewModel AccessClientForAddViewModel
        {
            get;
            private set;
        }
        public ClientsAtChatViewModel ClientsAtChatViewModel
        {
            get;
            private set;
        }

        public ControlVisibilityViewClientsViewModel(AccessClientForAddViewModel accessClientForAdd, ClientsAtChatViewModel clientsAtChatViewModel)
        {
            AccessClientForAddViewModel = accessClientForAdd;
            ClientsAtChatViewModel = clientsAtChatViewModel;
            ClientsAtChatViewModel.VisibilityClientsAtChat = Visibility.Visible;
            AccessClientForAddViewModel.VisibilityOfControlAllClient = Visibility.Hidden;
        }
    }
}
