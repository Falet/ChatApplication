namespace Common.Network.Packets
{
    using System.Collections.Generic;
    public class ConnectionResponse
	{
		#region Properties

		public ResultRequest Result { get; }

        public string Reason { get; }

        #endregion Properties

        #region Constructors

        public ConnectionResponse(ResultRequest result, string reason)
        {
            Result = result;
            Reason = reason;
        }

        #endregion Constructors
    }
}
