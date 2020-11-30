namespace TestServer
{
	using TestServer.Network;
	using System;
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
		}
		public void Start()
		{
			_server = TransportFactory.Create(_ConfigServer.Protocol);

			RequestManagerDb requestManagerDb = new RequestManagerDb();

			HandlerRequestFromServer handlerRequestFromServer = new HandlerRequestFromServer(_server, requestManagerDb.GetTables());

			ChangeDb changeDb = new ChangeDb(handlerRequestFromServer, requestManagerDb);

			_server.Start(new IPEndPoint(IPAddress.Any, _ConfigServer.Port));
			//Console.ReadLine();
		}
	}
}
