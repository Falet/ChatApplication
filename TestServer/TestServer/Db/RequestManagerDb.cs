namespace TestServer.Network
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Data.SqlClient;
    using TestServer.DbEF;
    using System.Linq;

    public class RequestManagerDb : IGetData
    {
        public List<UserProperties> GetBeginData()
        {
            List <UserProperties> listUserProperties = new List<UserProperties>();
            using (var db = new DBChat())
            {
                foreach (var item in db.PoolUsers)
                {
                    string Name = item.CharacterID;
                    UserProperties userProperties = new UserProperties();
                    userProperties.Login = Name;
                    Console.WriteLine(Name);
                    var RoomsOfUsers = db.Characters
                                         .Where(User => User.CharacterID == Name);
                    foreach (var Room in RoomsOfUsers)
                    {
                        Console.WriteLine(Room.RoomID);
                    }
                }
            }
            return new List<UserProperties>();
        }
        public List<string> GetAllNameUser()
        {
            List<string> buf = new List<string>();
            return buf;
        }

        public bool AddNewUser(string name)
        {
            throw new NotImplementedException();
        }

        public bool AddNewMessage(string message, int room)
        {
            throw new NotImplementedException();
        }

        public bool CreatNewRoom(string nameOwner, List<string> listNameOfUsers)
        {
            throw new NotImplementedException();
        }

        public bool AddUserToRoom(string name, int room)
        {
            throw new NotImplementedException();
        }

        public bool RemoveUserFromRoom(string name, int room)
        {
            throw new NotImplementedException();
        }

        public bool GetMessageFromRoom(int room)
        {
            throw new NotImplementedException();
        }
    }
}
