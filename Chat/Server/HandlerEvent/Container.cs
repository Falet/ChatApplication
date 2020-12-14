using Common.Network.Packets;
using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Network
{
    public static class Container
    {
        public static MessageContainer GetContainer(string identifier,object valueForContainer)
        {
            var container = new MessageContainer
            {
                Identifier = identifier,
                Payload = valueForContainer
            };

            return container;
        }
    }
}
