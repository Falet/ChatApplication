using Client.Model;
using Common.Network;
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
        private Visibility _visibilityViewClientsAtChat;
        public Visibility VisibilityClientsAtChat
        {
            get => _visibilityViewClientsAtChat;
            set => SetProperty(ref _visibilityViewClientsAtChat, value);
        }
        public ClientsAtChatViewModel(IHandlerConnection handlerConnection, IHandlerChats handlerChats, int numberChat, Dictionary<string, bool> clientForAdd)
        {
            handlerChats.AddedClientsToChat += OnClientAddedToChat;
        }
        public void OnClientAddedToChat(object sender, AddedClientsToChatEventArgs container)
        {

        }
    }
}
