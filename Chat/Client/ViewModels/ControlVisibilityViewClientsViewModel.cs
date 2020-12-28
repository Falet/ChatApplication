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
        public AllClientViewModel AllClientViewModel
        {
            get;
            private set;
        }
        public ClientsAtChatViewModel ClientsAtChatViewModel
        {
            get;
            private set;
        }

        public ControlVisibilityViewClientsViewModel(AllClientViewModel allClientViewModel, ClientsAtChatViewModel clientsAtChatViewModel)
        {
            AllClientViewModel = allClientViewModel;
            ClientsAtChatViewModel = clientsAtChatViewModel;
            ClientsAtChatViewModel.VisibilityClientsAtChat = Visibility.Visible;
            AllClientViewModel.VisibilityOfControlAllClient = Visibility.Hidden;
        }
    }
}
