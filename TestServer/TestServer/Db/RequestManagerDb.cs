using System;
using System.Collections.Generic;
using System.Text;

namespace TestServer.Network
{
    public static class RequestManagerDb
    {
        public static bool AddNewUser(string name)
        {
            return true;
        }
        /*public static bool IsExistUser(string name)
        {
            return true;
        }*/
        public static bool AddNewMessage(string message, int room)
        {
            return true;
        }
        public static bool CreatNewRoom(string nameOwner, List<string> listNameOfUsers)
        {
            return true;
        }
        public static bool AddUserToChat(string name, int room)
        {
            return true;
        }
        public static bool RemoveUserFromChat(string name, int room)
        {
            return true;
        }
        public static bool GetMessageFromChat(int room)
        {
            return true;
        }
        public static List<string> GetAllNameUser()
        {
            List<string> buf = new List<string>();
            return buf;
        }
    }
}
