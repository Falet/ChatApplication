namespace Server.DataBase
{
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Common.Network;
    using Network;
    public interface IGetOrSetData
    {
        #region Methods

        public ConcurrentDictionary<string, ClientProperties> GetInfoAboutAllClient();
        public ConcurrentDictionary<int, InfoChat> GetInfoAboutAllChat();
        public ConcurrentDictionary<int, List<MessageInfo>> GetAllMessageFromChats();
        public Task<bool> AddNewClient(ClientInfo container);
        public Task<bool> AddNewMessage(MessageInfoForDb container);
        public Task<int> CreatNewChat(CreatingChatInfo container);
        public Task<bool> RemoveChat(int numberChat);
        public Task<bool> AddClientToChat(AddClientToChat container);
        public Task<bool> RemoveClientFromChat(RemoveClientFromChat container);

        #endregion Methods
    }
}
