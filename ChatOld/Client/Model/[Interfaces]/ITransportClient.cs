using Common.Network.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Client.Model
{
    public interface ITransportClient
    {
        public void Connect(string ip, int port);
        public void Send(MessageContainer message);
    }
}
