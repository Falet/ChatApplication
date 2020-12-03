using System;
using System.Collections.Generic;
using System.Text;

namespace TestServer.Network
{
    public class MessageRequest
    {
        #region Properties

        public string ClientName { get; }
        public string Message { get; }
        public int Room { get; }

        #endregion Properties

        #region Constructors

        public MessageRequest(string clientName,string message, int room)
        {
            ClientName = clientName;
            Message = message;
            Room = room;
        }

        #endregion Constructors

        #region Methods

        public MessageContainer GetContainer()
        {
            var container = new MessageContainer
            {
                Identifier = nameof(MessageRequest),
                Payload = this
            };

            return container;
        }

        #endregion Methods
    }
}
