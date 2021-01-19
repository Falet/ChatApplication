using System;
using System.Collections.Generic;
using System.Text;

namespace Client.Model
{
    public class AnotherClientConnectedEventArgs
    {
        public string NameClient { get; }
        public AnotherClientConnectedEventArgs(string nameClient)
        {
            NameClient = nameClient;
        }
    }
}
