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
        public ControlNavigationChatsViewModel VisibilityControlNavigationChatsViewModel
        {
            get;
            private set;
        }
        public CreateChatViewModel VisibilityCreateChat
        {
            get;
            private set;
        }

        public ControlVisibilityElementsOfChatMenuViewModel(IHandlerMessages handlerMessages, IHandlerChats handlerChats)
        {
            VisibilityControlNavigationChatsViewModel = new ControlNavigationChatsViewModel(handlerMessages, handlerChats);
            VisibilityCreateChat = new CreateChatViewModel(handlerChats); 
            VisibilityControlNavigationChatsViewModel.VisibilityNavigationChat = Visibility.Visible;
            VisibilityCreateChat.VisibilityCreateChat = Visibility.Hidden;
        }
    }
}
