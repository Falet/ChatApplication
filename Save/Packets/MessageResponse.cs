using System;
using System.Collections.Generic;
using System.Text;

namespace TestServer.Network
{
    public class MessageResponse
    {
        #region Properties

        public string FromClientName { get; }
        public string Message { get; }
        public int Room { get; }

        #endregion Properties

        #region Constructors

        public MessageResponse(string clientName, string message, int room)
        {
            FromClientName = clientName;
            Message = message;
            Room = room;
        }

        #endregion Constructors

        #region Methods

        public MessageContainer GetContainer()
        {
            var container = new MessageContainer
            {
                Identifier = nameof(MessageResponse),
                Payload = this
            };

            return container;
        }

        #endregion Methods
    }
}
