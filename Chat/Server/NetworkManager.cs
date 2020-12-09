namespace Server
{
	using Server.Network;
	using Common.Configuration;
	using Common.Network;
	using Configuration;
	using DataBase;
	class NetworkManager
	{

		ITransportServer _server;
		ConfigServer _ConfigServer;

		#region Constructors

		public NetworkManager()
		{
			//Перебор всех методов получения конфига до получения результата
		}
		public NetworkManager(TypeReceivedConfig type)
		{
			_ConfigServer = ConfigurationServer.ReadConfigFromFile("user.json");
			Start();
		}

		#endregion Constructors

		#region Methods

		private void Start()
		{
			_server = TransportFactory.Create(_ConfigServer);

			RequestManagerDb requestManagerDb = new RequestManagerDb();

			HandlerRequestFromClient handlerRequestFromServer = new HandlerRequestFromClient(_server, requestManagerDb);

			_server.Start();
		}

		#endregion Methods
	}
}
