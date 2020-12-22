using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.ViewModels
{
    public class ControlTabChat : BindableBase
    {
        private ChatViewModel _selectedChat;

        public int NameTab { get; set; }
        public ChatViewModel ChangeSelectedChat
        {
            get => _selectedChat;
            set => SetProperty(ref _selectedChat, value);
        }
        public ControlTabChat()
        {
            _selectedChat = new ChatViewModel();
        }

    }
}
