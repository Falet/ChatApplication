using System;
using System.Collections.Generic;
using System.Text;

namespace TestServer.Network
{
    public class RemoveClientFromChat
    {
        public int Room { get; set; }
        public List<string> Users { get; set; }
    }
}
