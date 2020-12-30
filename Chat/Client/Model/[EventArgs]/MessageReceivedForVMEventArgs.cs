using Common.Network;
using System;
using System.Collections.Generic;
using System.Text;

namespace Client.Model
{
    public class MessageReceivedForVMEventArgs
    {
        #region Properties

        public MessageInfo Message { get; }

        public int NumberChat { get; }

        #endregion Properties

        #region Constructors

        public MessageReceivedForVMEventArgs(MessageInfo message, int numberChat)
        {
            Message = message;
            NumberChat = numberChat;
        }

        #endregion Constructors
    }
}
