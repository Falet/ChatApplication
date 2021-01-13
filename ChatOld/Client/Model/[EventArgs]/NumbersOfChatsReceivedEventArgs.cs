using Common.Network;
using System;
using System.Collections.Generic;
using System.Text;

namespace Client.Model
{
    public class NumbersOfChatsReceivedEventArgs
    {
        #region Properties

        public Dictionary<LinkNumberChatCreator, List<string>> AllInfoAboutChat { get; }
 

        #endregion Properties

        #region Constructors

        public NumbersOfChatsReceivedEventArgs(Dictionary<LinkNumberChatCreator, List<string>> allInfoAboutChat)
        {
            AllInfoAboutChat = allInfoAboutChat;
        }

        #endregion Constructors
    }
}
