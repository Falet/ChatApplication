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
        private Visibility _visibilityView;
        private IHandlerChats _handlerChats;
        private AllClientViewModel _allClientViewModel;
        public Visibility VisibilityCreateChat
        {
            get => _visibilityView;
            set => SetProperty(ref _visibilityView, value);
        }
        public AllClientViewModel AllClientViewModel
        {
            get => _allClientViewModel;
            set => SetProperty(ref _allClientViewModel, value);
        }
        public CreateChatViewModel(AllClientViewModel allClientViewModel, IHandlerConnection handlerConnection, IHandlerChats handlerChats)
        {
            _visibilityView = Visibility.Hidden;
            VisibilityCreateChat = Visibility.Hidden;
            _handlerChats = handlerChats;
            _allClientViewModel = allClientViewModel;
        }
    }
}
