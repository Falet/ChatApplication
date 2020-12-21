using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Model
{
    public class ClientInfo : IClientInfo
    {
        public string Login { get; private set; }
        public ClientInfo()
        {
            Login = "Ivar";
        }
        public void SetLogin(string nameClient)
        {
            Login = nameClient;
        }
    }
}
