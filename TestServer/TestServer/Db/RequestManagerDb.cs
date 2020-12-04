namespace TestServer.Network
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Data.SqlClient;
    using TestServer.DbEF;
    using System.Linq;
    using System.Threading;

    public class RequestManagerDb : IGetOrSetData
    {
        public Dictionary<string, UserProperties> GetUserInfo()
        {
            List <UserProperties> listUserProperties = new List<UserProperties>();
            using (var db = new DBChat())
            {
                foreach (var item in db.PoolUsers)
                {
                    string Name = item.CharacterID;
                    UserProperties userProperties = new UserProperties();
                    Console.WriteLine(Name);
                    var RoomsOfUsers = db.Characters
                                         .Where(User => User.CharacterID == Name);
                    foreach (var Room in RoomsOfUsers)
                    {
                        Console.WriteLine(Room.RoomID);
                    }
                }
            }
            return new Dictionary<string,UserProperties>();
        }
        public Dictionary<int, InfoAllChat> GetRoomInfo()
        {
            return new Dictionary<int, InfoAllChat>();
        }
        public Dictionary<int, List<MessageInfo>> GetAllMessage()
        {
            return new Dictionary<int, List<MessageInfo>>();
        }
        public List<string> GetAllNameUser()
        {
            List<string> buf = new List<string>();
            return buf;
        }

        public bool AddNewUser(ClientInfo client)
        {
            return true;
        }

        public bool AddNewMessage(MessageInfoForDb message)
        {
            return true;
        }

        public bool CreatNewRoom(CreatingChatInfo chatInfo)
        {
            return true;
        }
        public bool RemoveRoom(int room)
        {
            return true;
        }
        public bool AddUserToRoom(AddClientToChat InfoAboutAddedChat)
        {
            return true;
        }

        public bool RemoveUserFromRoom(RemoveClientFromChat InfoAboutRemovedChat)
        {
            return true;
        }

        public bool GetMessageFromRoom(int room)
        {
            return true;
        }


    }
}
