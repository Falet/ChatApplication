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

        public ControlVisibilityElementsOfChatMenuViewModel(IHandlerConnection handlerConnection, IHandlerMessages handlerMessages, IHandlerChats handlerChats)
        {
            ControlNavigationChatsViewModel = new ControlNavigationChatsViewModel(handlerConnection, handlerMessages, handlerChats);
            CreateChat = new CreateChatViewModel(handlerConnection, handlerChats);
            CreateChat.VisibilityCreateChat = Visibility.Hidden;
            ControlNavigationChatsViewModel.VisibilityNavigationChat = Visibility.Visible;
        }
    }
}
