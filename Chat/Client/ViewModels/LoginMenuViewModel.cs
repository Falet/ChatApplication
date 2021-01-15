namespace Client.ViewModels
{
    using Common.Network;
    using Client.Model;
    using Prism.Commands;
    using Prism.Mvvm;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Windows;
    public class LoginMenuViewModel : BindableBase
    {
        private Visibility _visibilityView;
        private string _comboBoxItemSelected;
        private string _textIP;
        private string _textPort;
        private string _textLogin;
        private string _textError;
        private IHandlerConnection _handlerConnection;
        private Regex regexIP;
        private Regex regexLogin;
        public Visibility VisibilityLoginMenu
        {
            get => _visibilityView;
            set => SetProperty(ref _visibilityView, value);
        }
        public string IP
        {
            get => _textIP;
            set => SetProperty(ref _textIP, value);
        }
        public string Port
        {
            get => _textPort;
            set => SetProperty(ref _textPort, value);
        }
        public string Login
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

        public string Protocol
        {
            get => _comboBoxItemSelected;
            set => SetProperty(ref _comboBoxItemSelected, value);
        }
        public LoginMenuViewModel(IHandlerConnection handlerConnection)
        {
            _visibilityView = Visibility.Visible;

            IP = "192.168.0.104";
            Port = "35";

            _textError = null;
            regexIP = new Regex(@"^((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?).){3}(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$");
            regexLogin = new Regex(@"^[A-Za-z0-9]+(?:[ _-][A-Za-z0-9]+)*$");

            _handlerConnection = handlerConnection;
            _handlerConnection.ClientConnected += OnClientConnected;

            SignIn = new DelegateCommand(ConnectToServer, IsTrueDataForSignIn);
            SignIn.ObservesProperty(() => IP);
            SignIn.ObservesProperty(() => Login);
            SignIn.ObservesProperty(() => Port);
        }
        private void ConnectToServer()
        {
            _handlerConnection.Connect(IP, Port, Protocol);
            _handlerConnection.Send(Login);
        }
        private bool IsTrueDataForSignIn()
        {
            if (Login == null)
            {
                return false;
            }
            if (IP == null)
            {
                return false;
            }
            string error = null;
            Match match = regexIP.Match(IP);
            if (string.IsNullOrWhiteSpace(IP))
            {
                error += "IP is required.\n";
            }
            else if (!match.Success)
            {
                error += "IP must be valid ip format. For example '192.168.1.0'\n";
            }

            if (string.IsNullOrWhiteSpace(Port))
            {
                error += "Port is required\n";
            }
            else if (!Port.All(char.IsDigit))
            {
                error += "Port must be valid\n";
            }

            
            match = regexLogin.Match(Login);
            if (string.IsNullOrWhiteSpace(Login))
            {
                error += "Login is required.\n";
            }
            else if (!match.Success)
            {
                error += "Login must be valid username format and contains only alphabetic symbols and numbers. For example 'Cyberprank2020'\n";
            }

            if(error!= null)
            {
                TextError = error;
                return false;
            }
            else
            {
                TextError = null;
                return true;
            }
        }
        private void OnClientConnected(object sender, ClientConnectedToServerEventArgs container)
        {
            if(container.Result == ResultRequest.Ok)
            {
                VisibilityLoginMenu = Visibility.Collapsed;
            }
            else
            {
                TextError = container.Reason;
            }
        }
    }
}
