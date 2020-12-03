using System;
using System.Collections.Generic;
using System.Text;

namespace TestServer.Network
{
    public class InfoAllChat
    {
        public int IdChat { get; }
        public string OwnerChat { get; }
        public InfoAllChat(int idChat, string ownerChat)
        {
            IdChat = idChat;
            OwnerChat = ownerChat;
        }
    }
}
