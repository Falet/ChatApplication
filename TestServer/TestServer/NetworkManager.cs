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
            ITransport Server = TransportFactory.Create(configServer.Protocol);

            Console.ReadLine();
        }
        public void Start()
        {

        }
    }
}
