using System;
using System.Collections.Generic;
using System.Text;

namespace TestServer.Network
{
    public class ConnectionResponse
	{
		#region Properties

		public ResultRequest Result { get; set; }

        public string Reason { get; set; }

        #endregion Properties

        #region Methods

        public MessageContainer GetContainer()
        {
            var container = new MessageContainer
            {
                Identifier = nameof(ConnectionResponse),
                Payload = this
            };

            return container;
        }

        #endregion Methods
    }
}
