using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Client.ViewModels
{
    public class LoginMenuViewModel : BindableBase
    {
        private Visibility _visibilityView = Visibility.Hidden;
        private ComboBoxItem _comboBoxItemSelected;
        private string _textIP;
        private string _textPort;
        private string _textLogin;
        private string _textError;

        public Visibility VisibilityLoginMenu
        {
            get => _visibilityView;
            set => SetProperty(ref _visibilityView, value);
        }
        public string TextIP
        {
            get => _textIP;
            set => SetProperty(ref _textIP, value);
        }
        public string TextPort
        {
            get => _textPort;
            set => SetProperty(ref _textPort, value);
        }
        public string TextLogin
        {
            get => _textLogin;
            set => SetProperty(ref _textLogin, value);
        }
        public string TextError
        {
            get => _textError;
            set => SetProperty(ref _textError, value);
        }
        public DelegateCommand SignIn { get; }

        public ComboBoxItem CurrentItemProtocol
        {
            get => _comboBoxItemSelected;
            set => SetProperty(ref _comboBoxItemSelected, value);
        }
    }
}
