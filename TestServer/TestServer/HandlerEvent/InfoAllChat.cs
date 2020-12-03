using System;
using System.Collections.Generic;
using System.Text;

namespace TestServer.Network
{
    public class InfoAllChat
    {
        public string OwnerChat { get; }
        public List<string> NameClients { get; }

        public InfoAllChat(string ownerChat, List<string> nameClients)
        {
            OwnerChat = ownerChat;
            NameClients = nameClients;
        }
    }
}
