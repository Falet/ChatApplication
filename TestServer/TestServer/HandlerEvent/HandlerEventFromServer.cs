using System;
using System.Collections.Generic;
using System.Text;

namespace TestServer.Network
{
    using System.Threading;
    using System.Threading.Tasks;
    public class HandlerRequestFromServer
    {
        #region Fields

        private List<UserProperties> _usersProperties;
        private List<InfoAllChat> _infoAllChat;
        private IGetData _data;
        private ITransport _server;

        #endregion Fields

        public event EventHandler<UserConnectedEventArgs> NewUserConnected;
        public event EventHandler<MessageReceivedEventArgs> NewMessageRecieved;
        public event EventHandler<AddedUsersToChatEventArgs> NewUsersAddedToChat;
        public event EventHandler<RemovedUsersFromChatEventArgs> UsersFromChatRemoved;
        public event EventHandler<AddedChatEventArgs> NewChatCreated;
        public event EventHandler<RemovedChatEventArgs> ChatRemoved;

        public HandlerRequestFromServer(ITransport server, IGetData data)
        {
            _server = server;

            _server.UserConnected += OnConnect;
            _server.UserDisconnected += OnDisconnect;
            _server.MessageReceived += OnMessage;
            _server.ConnectedToChat += OnChatOpened;
            _server.AddedChat += OnAddedChat;
            _server.RemovedChat += OnRemovedChat;
            _server.AddedUsersToChat += OnAddedUsersToChat;
            _server.RemovedUsersFromChat += OnRemovedUsersFromChat;

            _data = data;
            _usersProperties = _data.GetBeginData();
            _infoAllChat = new List<InfoAllChat>();
        }
        public void OnConnect(object sender, UserConnectedEventArgs container)
        {
            foreach (var connection in _usersProperties)
            {
                if (connection.Login == container.ClientName)
                {
                    if(connection.IdConnection == Guid.Empty)
                    {
                        connection.IdConnection = container.ClientId;
                    }
                    else
                    {
                        _server.FreeConnection(container.ClientId);
                    }
                }
                else
                {
                    NewUserConnected?.Invoke(this, container);
                }
            }
        }
        public void OnDisconnect(object sender, UserDisconnectedEventArgs container)
        {
            foreach (var connection in _usersProperties)
            {
                if (connection.Login == container.ClientName)
                {
                    if (connection.IdConnection != Guid.Empty)
                    {
                        connection.IdConnection = Guid.Empty;
                    }
                }
            }
        }
        public void OnMessage(object sender, MessageReceivedEventArgs container)
        {
            NewMessageRecieved?.Invoke(this, container);
        }
        public void OnChatOpened(object sender, ConnectionToChatEventArgs container)
        {

        }
        public async void OnAddedChat(object sender, AddedChatEventArgs container)
        {
            List<UserProperties> x = await Task.Run(() => _data.GetBeginData());
            _infoAllChat.Add(new InfoAllChat(_infoAllChat[_infoAllChat.Count - 1].IdChat + 1, container.ClientName));//или использовать Guid для нумерации комнат
            NewChatCreated?.Invoke(this, container);//Отправляет номер комнаты на создание в БД
        }
        public void OnRemovedChat(object sender, RemovedChatEventArgs container)
        {

        }
        public void OnAddedUsersToChat(object sender, AddedUsersToChatEventArgs container)
        {

        }
        public void OnRemovedUsersFromChat(object sender, RemovedUsersFromChatEventArgs container)
        {

        }
    }
}
