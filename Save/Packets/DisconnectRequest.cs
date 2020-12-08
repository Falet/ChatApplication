using System;
using System.Collections.Generic;
using System.Text;

namespace TestServer.Network
{
    public class DisconnectRequest
    {
        public string ClientName { get; }

        #region Constructors

        public DisconnectRequest(string clientName)
        {
            ClientName = clientName;
        }

        #endregion Constructors

        #region Methods

        public MessageContainer GetContainer()
        {
            var container = new MessageContainer
            {
                Identifier = nameof(DisconnectRequest),
                Payload = this
            };

            return container;
        }

        #endregion Methods
    }
}
