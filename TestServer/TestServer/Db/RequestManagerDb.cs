namespace TestServer.Network
{
    using System;
    using System.Collections.Generic;
    using TestServer.DbEF;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Concurrent;

    public class RequestManagerDb : IGetOrSetData
    {
        public ConcurrentDictionary<string, UserProperties> GetAllUserInfo()
        {
            ConcurrentDictionary<string, UserProperties> allUserInfo = new ConcurrentDictionary<string, UserProperties>();
            using (var db = new DBChat())
            {
                foreach (var item in db.PoolUsers)
                {
                    string Name = item.CharacterID;

                    List<int> numbersRoom = new List<int>();
                    var RoomsOfUsers = db.Characters
                                         .Where(User => User.CharacterID == Name);
                    foreach (var Room in RoomsOfUsers)
                    {
                        numbersRoom.Add(Room.RoomID);
                    }
                    allUserInfo.TryAdd(Name,new UserProperties { IdConnection = Guid.Empty, NumbersRoom = numbersRoom });
                }
            }
            return allUserInfo;
        }
        public ConcurrentDictionary<int, InfoRoom> GetAllRoomInfo()
        {
            ConcurrentDictionary<int, InfoRoom> allRoomInfo = new ConcurrentDictionary<int, InfoRoom>();
            using (var db = new DBChat())
            {
                foreach (var item in db.PoolRoom)
                {
                    int numberRoom = item.RoomID;

                    List<string> clientsName = new List<string>();
                    var UsersOfRooms = db.Characters
                                         .Where(User => User.RoomID == numberRoom);
                    foreach (var User in UsersOfRooms)
                    {
                        clientsName.Add(User.CharacterID);
                    }
                    allRoomInfo.TryAdd(numberRoom, new InfoRoom(item.OwnerRoom, clientsName));
                }
            }
            return allRoomInfo;
        }
        public ConcurrentDictionary<int, List<MessageInfo>> GetAllMessage()
        {
            ConcurrentDictionary<int, List<MessageInfo>> allMessage = new ConcurrentDictionary<int, List<MessageInfo>>();
            using (var db = new DBChat())
            {
                foreach (var item in db.Messages)
                {
                    int numberRoom = item.RoomID;

                    List<MessageInfo> messages = new List<MessageInfo>();
                    var MessagesOfRooms = db.Messages
                                         .Where(User => User.RoomID == numberRoom);
                    foreach (var message in MessagesOfRooms)
                    {
                        messages.Add(new MessageInfo { FromMessage = message.From, Text = message.Text, Time = DateTime.Parse(message.Time) });
                    }
                    allMessage.TryAdd(numberRoom, messages);
                }
            }
            return allMessage;
        }

        public async Task<bool> AddNewUser(ClientInfo container)
        {
            using (var db = new DBChat())
            {
                PoolCharacters user = new PoolCharacters
                {
                    CharacterID = container.ClientName,
                    Users = new List<Characters>(),
                };
                db.PoolUsers.Add(user);

                int taskDb = await db.SaveChangesAsync();
                if (taskDb == 0)
                {
                    return false;
                }
            }
            return true;
        }

        public async Task<bool> AddNewMessage(MessageInfoForDb container)
        {
            using (var db = new DBChat())
            {
                Messages message = new Messages
                {
                    RoomID = container.NumberRoom,
                    From = container.FromMessage,
                    Text = container.Text,
                    Time = container.Time.ToString(),
                };
                db.Messages.Add(message);

                int taskDb = await db.SaveChangesAsync();
                if (taskDb == 0)
                {
                    return false;
                }
            }
            return true;
        }

        public async Task<int> CreatNewRoom(CreatingChatInfo container)
        {
            int numberRoom = -1;
            using (var db = new DBChat())
            {
                Rooms room = new Rooms
                {
                    Type = "O",
                    OwnerRoom = container.ClientName,
                };
                db.PoolRoom.Add(room);

                Task<int> taskDb = db.SaveChangesAsync();
                taskDb.Wait();

                if (taskDb.Status == TaskStatus.Faulted)
                {
                    return numberRoom;
                }

                numberRoom = room.RoomID;
                if(await Task.Run(() => AddUserToRoom(new AddClientToChat { Room = numberRoom, Users = container.Clients })))
                {
                    //Вызов исключения на невозможность добавить пользователей в комнату
                }
            }
            return numberRoom;
        }
        public async Task<bool> RemoveRoom(int numberRoom)
        {
            using (var db = new DBChat())
            {
                Rooms room = new Rooms
                {
                    RoomID = numberRoom,
                };
                db.PoolRoom.Remove(room);

                int taskDb = await db.SaveChangesAsync();
                if (taskDb == 0)
                {
                    return false;
                }
            }
            return true;
        }
        public async Task<bool> AddUserToRoom(AddClientToChat container)
        {
            using (var db = new DBChat())
            {
                foreach(var user in container.Users)
                {
                    Characters userInRoom = new Characters
                    {
                        CharacterID = user,
                        RoomID = container.Room,
                    };
                    db.Characters.Add(userInRoom);
                }
                int taskDb = await db.SaveChangesAsync();
                if (taskDb == 0)
                {
                    return false;
                }
            }
            return true;
        }

        public async Task<bool> RemoveUserFromRoom(RemoveClientFromChat container)
        {
            using (var db = new DBChat())
            {
                foreach (var user in container.Users)
                {
                    Characters userInRoom = new Characters
                    {
                        CharacterID = user,
                        RoomID = container.Room,
                    };
                    db.Characters.Remove(userInRoom);
                }

                int taskDb = await db.SaveChangesAsync();
                if (taskDb == 0)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
