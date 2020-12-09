namespace Server.DataBase
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Concurrent;
    using Common.Network;
    using Network;
    public class RequestManagerDb : IGetOrSetData
    {
        #region Methods

        public ConcurrentDictionary<string, ClientProperties> GetInfoAboutAllClient()
        {
            ConcurrentDictionary<string, ClientProperties> allClientInfo = new ConcurrentDictionary<string, ClientProperties>();
            using (var db = new DBChat())
            {
                foreach (var item in db.PoolClients)
                {
                    List<int> numbersChat = new List<int>();
                    var ChatsOfClients = db.ClientsInChats
                                         .Where(Client => Client.ClientID == item.ClientID);
                    foreach (var Chat in ChatsOfClients)
                    {
                        numbersChat.Add(Chat.ChatID);
                    }
                    allClientInfo.TryAdd(item.ClientID, new ClientProperties { IdConnection = Guid.Empty, NumbersChat = numbersChat });
                }
            }
            return allClientInfo;
        }

        public ConcurrentDictionary<int, InfoChat> GetInfoAboutAllChat()
        {
            ConcurrentDictionary<int, InfoChat> allChatInfo = new ConcurrentDictionary<int, InfoChat>();
            using (var db = new DBChat())
            {
                foreach (var item in db.PoolChat)
                {
                    List<string> clientsName = new List<string>();
                    var ClientsOfChats = db.ClientsInChats
                                         .Where(Client => Client.ChatID == item.ChatID);
                    foreach (var Client in ClientsOfChats)
                    {
                        clientsName.Add(Client.ClientID);
                    }
                    allChatInfo.TryAdd(item.ChatID, new InfoChat { OwnerChat = item.OwnerChat, NameClients = clientsName });
                }
            }
            return allChatInfo;
        }
        public ConcurrentDictionary<int, List<MessageInfo>> GetAllMessageFromChats()
        {
            ConcurrentDictionary<int, List<MessageInfo>> allMessage = new ConcurrentDictionary<int, List<MessageInfo>>();
            using (var db = new DBChat())
            {
                foreach (var item in db.Messages)
                {
                    List<MessageInfo> messages = new List<MessageInfo>();
                    var MessagesOfChats = db.Messages
                                         .Where(Client => Client.ChatID == item.ChatID);
                    foreach (var message in MessagesOfChats)
                    {
                        messages.Add(new MessageInfo { FromMessage = message.From, Text = message.Text, Time = DateTime.Parse(message.Time) });
                    }
                    allMessage.TryAdd(item.ChatID, messages);
                }
            }
            return allMessage;
        }

        public async Task<bool> AddNewClient(ClientInfo container)
        {
            using (var db = new DBChat())
            {
                PoolClients client = new PoolClients
                {
                    ClientID = container.NameOfClient,
                    Clients = new List<ClientsInChats>(),
                };
                db.PoolClients.Add(client);

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
                    ChatID = container.NumberChat,
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

        public async Task<int> CreatNewChat(CreatingChatInfo container)
        {
            int numberChat = -1;
            using (var db = new DBChat())
            {
                Chats chat = new Chats
                {
                    Type = "O",
                    OwnerChat = container.NameOfClientSender,
                };
                db.PoolChat.Add(chat);

                Task<int> taskDb = db.SaveChangesAsync();
                taskDb.Wait();

                if (taskDb.Status == TaskStatus.Faulted)
                {
                    return numberChat;
                }

                if(await Task.Run(() => AddClientToChat(new AddClientToChat { NumberChat = chat.ChatID, NameOfClients = container.NameOfClients })))
                {
                    //Вызов исключения на невозможность добавить пользователей в комнату
                }
            }
            return numberChat;
        }
        public async Task<bool> RemoveChat(int numberChat)
        {
            using (var db = new DBChat())
            {
                Chats chat = new Chats
                {
                    ChatID = numberChat,
                };
                db.PoolChat.Remove(chat);

                int taskDb = await db.SaveChangesAsync();
                if (taskDb == 0)
                {
                    return false;
                }
            }
            return true;
        }
        public async Task<bool> AddClientToChat(AddClientToChat container)
        {
            using (var db = new DBChat())
            {
                foreach(var client in container.NameOfClients)
                {
                    ClientsInChats clientInChat = new ClientsInChats
                    {
                        ClientID = client,
                        ChatID = container.NumberChat,
                    };
                    db.ClientsInChats.Add(clientInChat);
                }
                int taskDb = await db.SaveChangesAsync();
                if (taskDb == 0)
                {
                    return false;
                }
            }
            return true;
        }

        public async Task<bool> RemoveClientFromChat(RemoveClientFromChat container)
        {
            using (var db = new DBChat())
            {
                foreach (var client in container.NameOfClients)
                {
                    ClientsInChats clientInChat = new ClientsInChats
                    {
                        ClientID = client,
                        ChatID = container.NumberChat,
                    };
                    db.ClientsInChats.Remove(clientInChat);
                }

                int taskDb = await db.SaveChangesAsync();
                if (taskDb == 0)
                {
                    return false;
                }
            }
            return true;
        }

        #endregion Methods
    }
}
