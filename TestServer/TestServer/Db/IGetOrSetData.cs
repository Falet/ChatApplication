using System;
using System.Collections.Generic;
using System.Text;

namespace TestServer.Network
{
    public interface IGetOrSetData
    {
        public Dictionary<string, UserProperties> GetUserInfo();
        public Dictionary<int, InfoAllChat> GetRoomInfo();
        public Dictionary<int, List<MessageInfo>> GetAllMessage();
        public bool AddNewUser(ClientInfo name);
        public bool AddNewMessage(MessageInfoForDb message);
        public bool CreatNewRoom(CreatingChatInfo chatInfo);
        public bool RemoveRoom(int room);
        public bool AddUserToRoom(AddClientToChat InfoAboutAddedChat);
        public bool RemoveUserFromRoom(RemoveClientFromChat InfoAboutRemovedChat);
        public bool GetMessageFromRoom(int room);
        public List<string> GetAllNameUser();
    }
}
