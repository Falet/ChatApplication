using System;
using System.Collections.Generic;
using System.Text;

namespace TestServer.Network
{
    public class MessageReceivedEventArgs
    {
        #region Properties

        public string ClientName { get; }

        public string Message { get; }

        public int Room { get; }

        #endregion Properties

        #region Constructors

        public MessageReceivedEventArgs(string clientName, string message, int room)
        {
            ClientName = clientName;
            Message = message;
            Room = room;
        }

        #endregion Constructors
    }
}
