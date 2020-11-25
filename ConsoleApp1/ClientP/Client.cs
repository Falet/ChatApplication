using System;
using System.Collections.Generic;
using System.Text;
using WebSocketSharp;

namespace Conso
{
    class Client
    {
        public void Connect()
        {
            WebSocket _socket;
            _socket = new WebSocket($"ws://192.168.37.106:65000/ChatGeneral");
            _socket.OnOpen += OnOpen;
            _socket.OnMessage += OnMessage;

            _socket.Connect();
        }
        private void OnOpen(object sender, System.EventArgs e)
        {
            
        }
        private void OnMessage(object sender, MessageEventArgs e)
        {
            Console.WriteLine(1);
        }
    }
}
