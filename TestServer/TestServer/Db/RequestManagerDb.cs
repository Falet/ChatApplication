namespace TestServer.Network
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Data.SqlClient;
    using TestServer.DbEF;
    public class RequestManagerDb
    {
        public List<UserProperties> GetTables()
        {
            using (var db = new DBChat())
            {
                foreach (var item in db.PoolUsers)
                {
                    string Name = item.CharacterID;
                    var RoomsOfUsers = db.Database.SqlQuery<Characters>("SELECT RoomID FROM Characters WHERE CharacterID = {0}", Name);
                    foreach (var Room in RoomsOfUsers)
                        Console.WriteLine(Room.RoomID);
                }
            }
            return new List<UserProperties>();
        }
        public bool AddNewUser(string name)
        {
            return true;
        }
        public bool AddNewMessage(string message, int room)
        {
            return true;
        }
        public bool CreatNewRoom(string nameOwner, List<string> listNameOfUsers)
        {
            return true;
        }
        public bool AddUserToChat(string name, int room)
        {
            return true;
        }
        public bool RemoveUserFromChat(string name, int room)
        {
            return true;
        }
        public bool GetMessageFromChat(int room)
        {
            return true;
        }
        public List<string> GetAllNameUser()
        {
            List<string> buf = new List<string>();
            return buf;
        }
    }
}
