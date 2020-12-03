using System;
using System.Collections.Generic;
using System.Text;

namespace TestServer.Network
{
    public class RemoveChatRequest
    {
        public string Sender { get; }

        public int Room { get; }

        #region Constructors

        public RemoveChatRequest(string sender, int room)
        {
            Sender = sender;
            Room = room;
        }

        #endregion Constructors

        #region Methods

        public MessageContainer GetContainer()
        {
            var container = new MessageContainer
            {
                Identifier = nameof(RemoveChatRequest),
                Payload = this
            };

            return container;
        }

        #endregion Methods
    }
}
