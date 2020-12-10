namespace Server
{
	using Server.Network;
	using Configuration;
	using Common.Network;
	using DataBase;
    using Unity;

    class NetworkManager
	{

		ITransportServer _server;
		ConfigServer _ConfigServer;
		IUnityContainer container = new UnityContainer();

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
			container.RegisterSingleton<WsServer>();
			//_server = TransportFactory.Create(_ConfigServer);

			RequestManagerDb requestManagerDb = new RequestManagerDb();

			HandlerRequestFromClient handlerRequestFromServer = new HandlerRequestFromClient(_server, requestManagerDb);

			_server.Start();
		}

		#endregion Methods
	}
}
