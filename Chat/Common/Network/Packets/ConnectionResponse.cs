namespace Common.Network.Packets
{
    using System.Collections.Generic;
    public class ConnectionResponse
	{
		#region Properties

		public ResultRequest Result { get; }

        public string Reason { get; }

        public Dictionary<int, string> InfoAboutChat { get; }//первый аргумент номер чата, второй - кто главный в чате
  
        public Dictionary<string,bool> InfoAboutClientActivity { get; }//первый аргумент имя пользователя, второй - активность

        #endregion Properties

        #region Constructors

        public ConnectionResponse(ResultRequest result, string reason, Dictionary<int, string> infoAboutChat, Dictionary<string, bool> infoAboutClientActivity)
        {
            Result = result;
            Reason = reason;
            InfoAboutChat = infoAboutChat;
            InfoAboutClientActivity = infoAboutClientActivity;
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
