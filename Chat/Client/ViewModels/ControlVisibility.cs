using Client.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Client.ViewModels
{
    using Prism.Commands;
    using Prism.Mvvm;
    using Prism.Events;
    public class ControlVisibility : BindableBase
    {
        private Visibility _loginVisibility = Visibility.Visible;
        private Visibility _navigationChatVisibility = Visibility.Hidden;

        public Visibility VisibilityLoginMenu
        {
            get => _loginVisibility;
            set => SetProperty(ref _loginVisibility,value);
        }

        public Visibility VisibilityNavigationChat
        {
            get => _navigationChatVisibility;
            set => SetProperty(ref _navigationChatVisibility, value);
        }

        public DelegateCommand SendCommand { get; }

        public ControlVisibility()
        {
            _navigationChatVisibility = Visibility.Hidden;
            SendCommand = new DelegateCommand(ExecuteSendCommand);
        }
        private void ExecuteSendCommand()
        {
            _navigationChatVisibility = Visibility.Hidden;
        }
    }
}
