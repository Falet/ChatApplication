using System;
using System.Collections.Generic;
using System.Text;

namespace TestServer.Network
{
    public interface IGetData
    {
        public Dictionary<string, UserProperties> GetUserInfo();
        public Dictionary<int, InfoAllChat> GetRoomInfo();
        public bool AddNewUser(string name);
        public bool AddNewMessage(MessageReceivedEventArgs message);
        public bool CreatNewRoom(string nameOwner, List<string> listNameOfUsers);
        public bool AddUserToRoom(string name, int room);
        public bool RemoveUserFromRoom(string name, int room);
        public bool GetMessageFromRoom(int room);
        public List<string> GetAllNameUser();
    }
}
