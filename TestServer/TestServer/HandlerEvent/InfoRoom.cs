using System;
using System.Collections.Generic;
using System.Text;

namespace TestServer.Network
{
    public class InfoRoom
    {
        public string OwnerChat { get; }
        public List<string> NameClients { get; }

        public InfoRoom(string ownerChat, List<string> nameClients)
        {
            OwnerChat = ownerChat;
            NameClients = nameClients;
        }
    }
}
