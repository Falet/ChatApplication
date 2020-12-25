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
    public class AddClientsAtChatViewModel : BindableBase
    {
        private Visibility _visibilityViewAddClients = Visibility.Hidden;
        public Visibility VisibilityOfControlAddClient
        {
            get => _visibilityViewAddClients;
            set => SetProperty(ref _visibilityViewAddClients, value);
        }
        public AddClientsAtChatViewModel(IHandlerConnection handlerConnection, IHandlerChats handlerChats)
        {

        }
    }
}
