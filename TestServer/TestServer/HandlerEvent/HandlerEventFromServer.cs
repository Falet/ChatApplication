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

        private Dictionary<string,UserProperties> _usersProperties;
        private Dictionary<int, InfoAllChat> _infoAllChat;
        private IGetData _data;
        private ITransport _server;

        #endregion Fields


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
            _usersProperties = _data.GetUserInfo();
            _infoAllChat = _data.GetRoomInfo();
        }
        public void OnConnect(object sender, UserConnectedEventArgs container)
        {
            if (_usersProperties.TryGetValue(container.ClientName,out UserProperties userProperties))
            {
                if(userProperties.IdConnection == Guid.Empty)
                {
                    userProperties.IdConnection = container.ClientId;
                }
                else
                {
                    _server.FreeConnection(container.ClientId);
                }
            }
            else
            {
                _usersProperties.Add(container.ClientName, new UserProperties { IdConnection = container.ClientId, Room = new List<InfoChat>() });
                
            }
        }
        public void OnDisconnect(object sender, UserDisconnectedEventArgs container)
        {
            if (_usersProperties.Remove(container.ClientName))
            {
                //Данного клиента не существует(возможно стоит убрать)
            }
        }
        public async void OnMessage(object sender, MessageReceivedEventArgs container)
        {
            if(await Task.Run(() => _data.AddNewMessage(container)) == false)
            {
                //Сообщение не удалось добавить, сигнал серверу на запрет приему сообщений до добавления сообщения
            }

            List<Guid> idClentsForSendMessage = new List<Guid>();
            foreach (var nameClient in _infoAllChat[container.Room].NameClients)
            {
                if (_usersProperties.TryGetValue(nameClient,out UserProperties guidClient))
                {
                    idClentsForSendMessage.Add(guidClient.IdConnection);
                }
            }

            MessageContainer messageForUsersAtRoom = new MessageRequest(container.ClientName, container.Message, container.Room).GetContainer();
            _server.Send(idClentsForSendMessage, messageForUsersAtRoom);
        }
        public void OnChatOpened(object sender, ConnectionToChatEventArgs container)
        {

        }
        public async void OnAddedChat(object sender, AddedChatEventArgs container)
        {
            
            //_infoAllChat.Add(new InfoAllChat(_infoAllChat[_infoAllChat.Count - 1].IdChat + 1, container.ClientName));//или использовать Guid для нумерации комнат
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
