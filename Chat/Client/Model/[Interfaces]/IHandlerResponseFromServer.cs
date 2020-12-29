using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Network;
using Common.Network.Packets;

namespace Client.Model
{
    public interface IHandlerResponseFromServer
    {
        public void ParsePacket(MessageContainer container);
    }
}
