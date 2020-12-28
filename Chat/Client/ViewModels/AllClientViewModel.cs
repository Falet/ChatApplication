using Client.Model;
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
    public class AllClientViewModel : BindableBase
    {
        private Visibility _visibilityViewAllClient;
        private ObservableCollection<InfoAboutClient> _clientsCollection;
        public Visibility VisibilityOfControlAllClient
        {
            get => _visibilityViewAllClient;
            set => SetProperty(ref _visibilityViewAllClient, value);
        }
        public ObservableCollection<InfoAboutClient> ClientsCollection
        {
            get => _clientsCollection;
            set => SetProperty(ref _clientsCollection, value);
        }
        public DelegateCommand AddClientToChat { get; }

        public AllClientViewModel(IHandlerConnection handlerConnection, IHandlerChats handlerChats)
        {
            handlerConnection.ReceivedInfoAboutAllClients += OnReceivedInfoAllClients;
            AddClientToChat = new DelegateCommand(AddClientToChatSendToServer);
        }
        private void OnReceivedInfoAllClients(object sender, ReceivedInfoAboutAllClientsEventArgs container)
        {

        }
        private void AddClientToChatSendToServer()
        {
            
        }
    }
}
