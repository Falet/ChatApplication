using Common.Network;
using System;
using System.Collections.Generic;
using System.Text;

namespace Client.Model
{
    public class NumbersOfChatsReceivedEventArgs
    {
        #region Properties

        public List<InfoAboutChat> AllInfoAboutChat { get; }
 

        #endregion Properties

        #region Constructors

        public NumbersOfChatsReceivedEventArgs(List<InfoAboutChat> allInfoAboutChat)
        {
            AllInfoAboutChat = allInfoAboutChat;
        }

        #endregion Constructors
    }
}
