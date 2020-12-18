using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Client.ViewModels
{
    public class ClientsAtChatViewModel : BindableBase
    {
        private Visibility _visibilityView = Visibility.Visible;
        public Visibility VisibilityClientsAtChat
        {
            get => _visibilityView;
            set => SetProperty(ref _visibilityView, value);
        }
    }
}
