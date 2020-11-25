using System;
using System.Net;

namespace TestServer
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine($"WebSocketServer: {IPAddress.Any}:{65000}");
            Connection Sda = new Connection();
            Sda.Start();
            Console.ReadLine();
        }
    }
}
