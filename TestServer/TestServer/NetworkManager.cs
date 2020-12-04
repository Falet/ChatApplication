namespace TestServer
{
	using TestServer.Network;
	using System;
	using System.Collections.Generic;
	using System.Net;
	class NetworkManager
	{

		ITransport _server;

		ConfigServer _ConfigServer;

		public NetworkManager()
		{
			//Перебор всех методов получения методов конфига до получения результата
		}
		public NetworkManager(TypeGettingConfig type)
		{
			_ConfigServer = ConfigurationServer.ReadConfigFromFile("user.json");
			Start();
		}
		private void Start()
		{
			_server = TransportFactory.Create(_ConfigServer);

			RequestManagerDb requestManagerDb = new RequestManagerDb();

			HandlerRequestFromServer handlerRequestFromServer = new HandlerRequestFromServer(_server, requestManagerDb);

			_server.Start();//*/
		}
	}
}
