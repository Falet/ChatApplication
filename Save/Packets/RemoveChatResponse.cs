using System;
using System.Collections.Generic;
using System.Text;

namespace TestServer.Network
{
    public class RemoveChatResponse
    {
        public int Room { get; }

        #region Constructors

        public RemoveChatResponse(int room)
        {
            Room = room;
        }

        #endregion Constructors

        #region Methods

        public MessageContainer GetContainer()
        {
            var container = new MessageContainer
            {
                Identifier = nameof(RemoveChatResponse),
                Payload = this
            };

            return container;
        }

        #endregion Methods
    }
}
