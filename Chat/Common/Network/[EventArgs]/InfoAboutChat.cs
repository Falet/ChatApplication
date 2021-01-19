using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Network
{
    public class InfoAboutChat
    {
        public int NumberChat { get; }
        public string NameCreator { get; }
        public List<string> NamesOfClients { get; }
        public InfoAboutChat(int numberChat, string nameCreator, List<string> namesOfClients)
        {
            NamesOfClients = namesOfClients;
            NumberChat = numberChat;
            NameCreator = nameCreator;
        }
    }
}
