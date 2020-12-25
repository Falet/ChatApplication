using Client.Model;
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
    public class ClientsAtChatViewModel : BindableBase
    {
        private Visibility _visibilityViewClientsAtChat = Visibility.Hidden; 
        //private ObservableCollection<string> 
        public Visibility VisibilityClientsAtChat
        {
            get => _visibilityViewClientsAtChat;
            set => SetProperty(ref _visibilityViewClientsAtChat, value);
        }
        public ClientsAtChatViewModel(IHandlerConnection handlerConnection, IHandlerChats handlerChats)
        {

        }
    }
}
