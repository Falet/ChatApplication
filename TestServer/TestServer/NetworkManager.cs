namespace TestServer
{
    using TestServer.Network;
    class NetworkManager
    {
        public NetworkManager()
        {
            //Перебор всех методов получения методов конфига до получения результата
        }
        public NetworkManager(TypeGettingConfig type)
        {
            ConfigurationServer configServer = new ConfigurationServer(type);

        }
        public void Start()
        {

        }
    }
}
