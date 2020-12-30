using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Network
{
    public class LinkNumberChatCreator
    {
        public int NumberChat { get; }
        public string NameCreator { get; }
        public LinkNumberChatCreator(int numberChat, string nameCreator)
        {
            NumberChat = numberChat;
            NameCreator = nameCreator;
        }
    }
}
