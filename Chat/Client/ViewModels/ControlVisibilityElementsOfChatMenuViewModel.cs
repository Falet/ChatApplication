using Client.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Client.ViewModels
{
    public class ControlVisibilityElementsOfChatMenuViewModel
    {
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

        public ControlVisibilityElementsOfChatMenuViewModel(AllClientViewModel allClientViewModel, IHandlerConnection handlerConnection, IHandlerMessages handlerMessages, IHandlerChats handlerChats)
        {
            ControlNavigationChatsViewModel = new ControlNavigationChatsViewModel(allClientViewModel, handlerConnection, handlerMessages, handlerChats);
            CreateChat = new CreateChatViewModel(allClientViewModel, handlerConnection, handlerChats);
            CreateChat.VisibilityCreateChat = Visibility.Hidden;
            ControlNavigationChatsViewModel.VisibilityNavigationChat = Visibility.Visible;
        }
    }
}
