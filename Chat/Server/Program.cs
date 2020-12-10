namespace Server
{
    using System;
    using System.Net;
    using Configuration;
    using Unity;
    using Common.Network;
    using DataBase;
    using Network;
    using Unity.Injection;

    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //var networkManager = new NetworkManager(TypeReceivedConfig.File);
                IUnityContainer container = new UnityContainer();

                ConfigServer _ConfigServer = ConfigurationServer.ReadConfigFromFile("user.json");
                container.RegisterSingleton<ITransportServer, WsServer>(new InjectionConstructor(new IPEndPoint(IPAddress.Any, _ConfigServer.Port)));

                container.RegisterType<IHandlerRequestToData, RequestManagerDb>();
                container.RegisterType<HandlerRequestFromClient>();
                container.Resolve<HandlerRequestFromClient>();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                Console.ReadLine();
            }
        }
    }
}
