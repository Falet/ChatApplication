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
            ConfigServer Buf = new ConfigurationServer().ReadConfigFromFile("user.json");
            ITransport Server = TransportFactory.Create(Buf.Protocol);
            Console.ReadLine();
        }
        public void Start()
        {

        }
    }
}
