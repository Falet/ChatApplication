using System;
using System.Collections.Generic;
using System.Text;

namespace TestServer.Network
{
    public class InfoChat
    {
        public int Room { get; }
        public string OwnerRoom { get; }
        public InfoChat(int room, string ownerRoom)
        {
            Room = room;
            OwnerRoom = ownerRoom;
        }
    }
}
