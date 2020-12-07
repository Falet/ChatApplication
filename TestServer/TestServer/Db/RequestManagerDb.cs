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
        public Dictionary<int, InfoRoom> GetRoomInfo()
        {
            return new Dictionary<int, InfoRoom>();
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

        public bool AddNewUser(ClientInfo container)
        {
            return true;
        }

        public bool AddNewMessage(MessageInfoForDb container)
        {
            return true;
        }

        public int CreatNewRoom(CreatingChatInfo container)
        {
            int numberRoom = -1;
            return numberRoom;
        }
        public bool RemoveRoom(int room)
        {
            return true;
        }
        public bool AddUserToRoom(AddClientToChat container)
        {
            return true;
        }

        public bool RemoveUserFromRoom(RemoveClientFromChat container)
        {
            return true;
        }

        public bool GetMessageFromRoom(int room)
        {
            return true;
        }


    }
}
