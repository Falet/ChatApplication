namespace TestServer
{
    using TestServer.Network;
    using System;
    class NetworkManager
    {
        public NetworkManager()
        {
            //Перебор всех методов получения методов конфига до получения результата
        }
        public NetworkManager(TypeGettingConfig type)
        {
            ConfigServer configServer = new ConfigurationServer().ReadConfigFromFile("user.json");

            ITransport server = TransportFactory.Create(configServer.Protocol);
            server.SetDictionaryOfUsers(RequestManagerDb.GetAllNameUser());

            HandlerRequest handlerRequest = new HandlerRequest(server);

            ChangeDb changeDb = new ChangeDb(handlerRequest);


            Console.ReadLine();
        }
        public void Start()
        {

        }
    }
}
