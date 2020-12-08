using System;
using System.Collections.Generic;
using System.Text;

namespace TestServer.Network
{
    public class ConnectionResponse
	{
		#region Properties

		public ResultRequest Result { get; }

        public string Reason { get; }

        public List<int> NumberRoom;

        #endregion Properties

        #region Constructors

        public ConnectionResponse(ResultRequest result, string reason, List<int> numberRoom)
        {
            Result = result;
            Reason = reason;
            NumberRoom = numberRoom;
        }

        #endregion Constructors

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
