namespace Server.HandlerEvent
{
    using Common.Network;
    using Common.Network.Packets;
    using Server.DataBase;
    using Server.Network;
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class HandlerMessage
    {
        #region Fields

        private ConcurrentDictionary<int, List<MessageInfo>> _MessagesAtChat;//Ключ - номер комнаты
        private IHandlerRequestToData _data;
        private ITransportServer _server;
		private HandlerConnection _connection;
		private HandlerChat _chats;

		#endregion Fields
		public HandlerMessage(ITransportServer server, IHandlerRequestToData data, HandlerConnection connection, HandlerChat chats)
        {
            _server = server;
            _server.MessageReceived += OnMessage;

			_connection = connection;
			_chats = chats;
		}

		public async void OnMessage(object sender, MessageReceivedEventArgs container)
		{
			if (_connection.cachedClientName.ContainsKey(container.NameOfClient)
			   && _chats.InfoChats.TryGetValue(container.NumberChat, out InfoChat infoChat))
			{
				List<Guid> idClientsForSendMessage = new List<Guid>();//Создание списка id для рассылки им сообщений
				foreach (var nameClient in infoChat.NameOfClients)
				{
					if (_connection.cachedClientName.TryGetValue(nameClient, out Guid clientGuid) && clientGuid != Guid.Empty)
					{
						idClientsForSendMessage.Add(clientGuid);
					}
				}
				await Task.Run(() => _server.Send(idClientsForSendMessage, new MessageResponse(container.NameOfClient, container.Message, container.NumberChat).GetContainer()));

				DateTime time = DateTime.Now;

				if (_MessagesAtChat.TryGetValue(container.NumberChat, out List<MessageInfo> allMessageAtChat))
				{
					var lastValueMessages = allMessageAtChat;
					allMessageAtChat.Add(new MessageInfo { FromMessage = container.NameOfClient, Text = container.Message, Time = time });
					_MessagesAtChat.TryUpdate(container.NumberChat, allMessageAtChat, lastValueMessages);
				}

				if (!await Task.Run(() => _data.AddNewMessage(new MessageInfoForDb
				{
					NumberChat = container.NumberChat,
					FromMessage = container.NameOfClient,
					Text = container.Message,
					Time = time
				})))
				{
					//Сообщение не удалось добавить, сигнал серверу на запрет приема сообщений до добавления сообщения
				}
			}
			else
			{
				//Ошибка, не существует либо пользователя, либо комнаты
			}
		}
	}
}
