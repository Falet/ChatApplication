namespace Server.DataBase
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Common.Network;
    using Network;
    public interface IHandlerRequestToData
    {
        #region Methods

        ConcurrentDictionary<string, Guid> GetInfoAboutAllClient();
        ConcurrentDictionary<string, ClientProperties> GetInfoAboutLinkClientToChat();
        ConcurrentDictionary<int, InfoChat> GetInfoAboutAllChat();
        ConcurrentDictionary<int, List<MessageInfo>> GetAllMessageFromChats();
        Task<bool> AddNewClient(ClientInfo container);
        Task<bool> AddNewMessage(MessageInfoForDataBase container);
        int CreatNewChat(CreatingChatInfo container);
        Task<bool> RemoveChat(int numberChat);
        Task<bool> AddClientToChat(AddClientToChat container);
        Task<bool> RemoveClientFromChat(RemoveClientFromChat container);

        #endregion Methods
    }
}
