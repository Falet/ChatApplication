﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Client.Model
{
    using Common.Network;
    public interface IHandlerChats
    {
        public event EventHandler<AddedChatEventArgs> AddedChat;
        public event EventHandler<RemovedChatEventArgs> RemovedChat;
        public event EventHandler<AddedClientsToChatEventArgs> AddedClientsToChat;
        public event EventHandler<RemovedClientsFromChatEventArgs> RemovedClientsFromChat;
        public event EventHandler<NumbersOfChatsReceivedEventArgs> ResponseNumbersChats;

        public void Send();
    }
}