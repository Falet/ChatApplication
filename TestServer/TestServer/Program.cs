using System;
namespace TestServer
{
    
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var networkManager = new NetworkManager(TypeGettingConfig.Console);
                networkManager.Start();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                Console.ReadLine();
            }
        }
    }
}
