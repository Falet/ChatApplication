using System;
using System.Collections.Generic;
using System.Text;

namespace TestServer.Network
{
    public class MessageContainer
    {
        public string Identifier { get; set; }
        public object Payload { get; set; }
    }
}
