using System;
using System.Collections.Generic;
using System.Text;

namespace TestServer.Network
{
    public class MessageInfoForDb
    {
        public int NumberRoom { get; set; }
        public string FromMessage { get; set; }
        public string Text { get; set; }
        public DateTime Time { get; set; }
    }
}
