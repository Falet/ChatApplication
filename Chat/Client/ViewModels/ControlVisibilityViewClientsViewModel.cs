namespace Client.ViewModels
{
    using Prism.Mvvm;
    using System.Windows;

    public class ControlVisibilityViewClientsViewModel : BindableBase
    {
        #region Properties
        public AccessableClientForAddViewModel AccessClientForAddViewModel
        {
            get;
            private set;
        }
        public ClientsAtChatViewModel ClientsAtChatViewModel
        {
            get;
            private set;
        }

        #endregion Properties

        #region Constructors

        public ControlVisibilityViewClientsViewModel(AccessableClientForAddViewModel accessClientForAdd, ClientsAtChatViewModel clientsAtChatViewModel)
        {
            AccessClientForAddViewModel = accessClientForAdd;
            ClientsAtChatViewModel = clientsAtChatViewModel;
            ClientsAtChatViewModel.VisibilityClientsAtChat = Visibility.Visible;
            AccessClientForAddViewModel.VisibilityOfControlAllClient = Visibility.Hidden;
        }

        #endregion Constructors
    }
}
