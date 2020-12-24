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
    public class CreateChatViewModel : BindableBase
    {
        private Visibility _visibilityView = Visibility.Visible;
        private IHandlerChats _handlerChats;
        public Visibility VisibilityCreateChat
        {
            get => _visibilityView;
            set => SetProperty(ref _visibilityView, value);
        }
        public CreateChatViewModel(IHandlerChats handlerChats)
        {
            _handlerChats = handlerChats;
        }
    }
}
