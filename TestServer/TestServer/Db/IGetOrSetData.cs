using System;
using System.Collections.Generic;
using System.Text;

namespace TestServer.Network
{
    public interface IGetOrSetData
    {
        public Dictionary<string, UserProperties> GetUserInfo();
        public Dictionary<int, InfoRoom> GetRoomInfo();
        public Dictionary<int, List<MessageInfo>> GetAllMessage();
        public bool AddNewUser(ClientInfo container);
        public bool AddNewMessage(MessageInfoForDb container);
        public int CreatNewRoom(CreatingChatInfo container);
        public bool RemoveRoom(int room);
        public bool AddUserToRoom(AddClientToChat container);
        public bool RemoveUserFromRoom(RemoveClientFromChat container);
        public bool GetMessageFromRoom(int room);
        public List<string> GetAllNameUser();
    }
}
