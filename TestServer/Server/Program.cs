
namespace Server
{
    using System;
    using Common.Network;
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var networkManager = new NetworkManager(TypeReceivedConfig.File);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                Console.ReadLine();
            }
        }
    }
}
