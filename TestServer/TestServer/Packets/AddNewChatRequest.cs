using System;
using System.Collections.Generic;
using System.Text;

namespace TestServer.Packets
{
    class AddNewChatRequest
    {
        public string Login;

        public List<string> Users;
    }
}
