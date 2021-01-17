namespace Client.ViewModels
{
    using Client.Model;
    using System.Windows;

    public class ControlVisibilityElementsOfChatMenuViewModel
    {
        #region Properties

        public ControlNavigationChatsViewModel ControlNavigationChatsViewModel
        {
            get;
            private set;
        }
        public CreateChatViewModel CreateChat
        {
            get;
            private set;
        }

        #endregion Properties

        #region Constructors

        public ControlVisibilityElementsOfChatMenuViewModel(IHandlerConnection handlerConnection, IHandlerMessages handlerMessages, IHandlerChats handlerChats)
        {
            ControlNavigationChatsViewModel = new ControlNavigationChatsViewModel(handlerConnection, handlerMessages, handlerChats);
            CreateChat = new CreateChatViewModel(handlerConnection, handlerChats);
            CreateChat.VisibilityCreateChat = Visibility.Hidden;
            ControlNavigationChatsViewModel.VisibilityNavigationChat = Visibility.Visible;
        }

        #endregion Constructors
    }
}
