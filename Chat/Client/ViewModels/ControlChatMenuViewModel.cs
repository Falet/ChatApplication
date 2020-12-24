using Client.Model;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Client.ViewModels
{
    public class ControlChatMenuViewModel : BindableBase
    {
        private ControlVisibilityElementsOfChatMenuViewModel _controlVisibilityElements;
        private IHandlerConnection _handlerConnection;
        private IHandlerMessages _handlerMessages;
        private IHandlerChats _handlerChats;
        private string _textButtonChangeViewClients;
        private string _textToolTip;
        private bool IsViewClientsChanged;
        private Visibility _visibilityView;

        public Visibility VisibilityChatMenu
        {
            get => _visibilityView;
            set => SetProperty(ref _visibilityView, value);
        }
        public string TextButtonChangeViewChatMenu
        {
            get => _textButtonChangeViewClients;
            set => SetProperty(ref _textButtonChangeViewClients, value);
        }
        public ControlVisibilityElementsOfChatMenuViewModel ControlVisibilityElements
        {
            get => _controlVisibilityElements;
            set => SetProperty(ref _controlVisibilityElements, value);
        }
        public string TextToolTip
        {
            get => _textToolTip;
            set => SetProperty(ref _textToolTip, value);
        }
        public DelegateCommand CreateChat { get; }
        public ControlChatMenuViewModel(IHandlerConnection handlerConnection, IHandlerMessages handlerMessages, IHandlerChats handlerChats)
        {
            _visibilityView = Visibility.Hidden;

            _handlerConnection = handlerConnection;
            _handlerMessages = handlerMessages;
            _handlerChats = handlerChats;

            _controlVisibilityElements = new ControlVisibilityElementsOfChatMenuViewModel(handlerMessages, handlerChats);
            _textButtonChangeViewClients = "Создать";
            _textToolTip = "Создать чат";
            IsViewClientsChanged = false;

            CreateChat = new DelegateCommand(ChangeViewClients).ObservesProperty(() => TextButtonChangeViewChatMenu);
            CreateChat.ObservesProperty(() => TextToolTip);
        }
        private void ChangeViewClients()
        {
            IsViewClientsChanged = !IsViewClientsChanged;
            if (IsViewClientsChanged)
            {
                TextButtonChangeViewChatMenu = "Назад";
                ControlVisibilityElements.VisibilityControlNavigationChatsViewModel.VisibilityNavigationChat = Visibility.Hidden;
                ControlVisibilityElements.VisibilityCreateChat.VisibilityCreateChat = Visibility.Visible;
                TextToolTip = "Назад к списку чатов";
            }
            else
            {
                TextButtonChangeViewChatMenu = "Добавить";
                ControlVisibilityElements.VisibilityControlNavigationChatsViewModel.VisibilityNavigationChat = Visibility.Visible;
                ControlVisibilityElements.VisibilityCreateChat.VisibilityCreateChat = Visibility.Hidden;
                TextToolTip = "Создать чат";
            }
        }
    }
}
