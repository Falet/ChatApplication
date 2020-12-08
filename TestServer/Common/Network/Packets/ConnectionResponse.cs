namespace Common.Network.Packets
{
    using System.Collections.Generic;
    public class ConnectionResponse
	{
		#region Properties

		public ResultRequest Result { get; }

        public string Reason { get; }

        public List<int> NumbersChat;

        #endregion Properties

        #region Constructors

        public ConnectionResponse(ResultRequest result, string reason, List<int> numbersChat)
        {
            Result = result;
            Reason = reason;
            NumbersChat = numbersChat;
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
