using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TestServer.Network
{
    public interface IGetOrSetData
    {
        public ConcurrentDictionary<string, UserProperties> GetAllUserInfo();
        public ConcurrentDictionary<int, InfoRoom> GetAllRoomInfo();
        public ConcurrentDictionary<int, List<MessageInfo>> GetAllMessage();
        public Task<bool> AddNewUser(ClientInfo container);
        public Task<bool> AddNewMessage(MessageInfoForDb container);
        public Task<int> CreatNewRoom(CreatingChatInfo container);
        public Task<bool> RemoveRoom(int numberRoom);
        public Task<bool> AddUserToRoom(AddClientToChat container);
        public Task<bool> RemoveUserFromRoom(RemoveClientFromChat container);
    }
}
