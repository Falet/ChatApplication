namespace Client.Model
{
    using Client.Model.Event;
    using Common.Network;
    using Common.Network.Packets;
    using Newtonsoft.Json.Linq;
    using System;

    public class HandlerResponseFromServer : IHandlerResponseFromServer
    {
        #region Event

        public event EventHandler<ClientConnectedToServerVmEventArgs> ClientConnected;
        public event EventHandler<AnotherClientConnectedVmEventArgs> AnotherClientConnected;
        public event EventHandler<AnotherClientDisconnectedVmEventArgs> AnotherClientDisconnected;
        public event EventHandler<AddedNewChatModelEventArgs> AddedChat;
        public event EventHandler<RemovedChatEventArgs> RemovedChat;
        public event EventHandler<AddedClientsToChatEventArgs> AddedClientsToChat;
        public event EventHandler<RemovedClientsFromChatEventArgs> RemovedClientsFromChat;
        public event EventHandler<MessageReceivedVmEventArgs> MessageReceived;
        public event EventHandler<ClientConnectedToChatVmEventArgs> ConnectedToChat;
        public event EventHandler<NumbersOfChatsReceivedModelEventArgs> ResponseNumbersChats;
        public event EventHandler<ReceivedInfoAboutAllClientsVmEventArgs> ReceivedInfoAboutAllClients;

        #endregion Event


        #region Methods

        public void ParsePacket(MessageContainer container)
        {
            switch (container.Identifier)
            {
                case nameof(ConnectionResponse):
                    {
                        var connectionResponse = ((JObject)container.Payload).ToObject(typeof(ConnectionResponse)) as ConnectionResponse;
                        ClientConnected?.Invoke(this, new ClientConnectedToServerVmEventArgs(connectionResponse.Result, connectionResponse.Reason));
                        break;
                    }
                case nameof(ConnectionNotice):
                    {
                        var connectionNoticeForClients = ((JObject)container.Payload).ToObject(typeof(ConnectionNotice)) as ConnectionNotice;
                        AnotherClientConnected?.Invoke(this, new AnotherClientConnectedVmEventArgs(connectionNoticeForClients.NameClient));
                        break;
                    }
                case nameof(DisconnectNotice):
                    {
                        var disconnectionResponse = ((JObject)container.Payload).ToObject(typeof(DisconnectNotice)) as DisconnectNotice;
                        AnotherClientDisconnected?.Invoke(this, new AnotherClientDisconnectedVmEventArgs(disconnectionResponse.NameClient));
                        break;
                    }
                case nameof(MessageResponse):
                    {
                        var messageResponse = ((JObject)container.Payload).ToObject(typeof(MessageResponse)) as MessageResponse;
                        MessageReceived?.Invoke(this, new MessageReceivedVmEventArgs(messageResponse.Message, messageResponse.NumberChat));
                        break;
                    }
                case nameof(ConnectToChatResponse):
                    {
                        var connectionToChatResponse = ((JObject)container.Payload).ToObject(typeof(ConnectToChatResponse)) as ConnectToChatResponse;
                        ConnectedToChat?.Invoke(this, new ClientConnectedToChatVmEventArgs(connectionToChatResponse.AllMessageFromChat, connectionToChatResponse.NumberChat));
                        break;
                    }
                case nameof(AddChatResponse):
                    {
                        var addNewChatResponse = ((JObject)container.Payload).ToObject(typeof(AddChatResponse)) as AddChatResponse;
                        AddedChat?.Invoke(this, new AddedNewChatModelEventArgs(addNewChatResponse.ClientCreator, addNewChatResponse.NumberChat, addNewChatResponse.Clients));
                        break;
                    }
                case nameof(RemoveChatResponse):
                    {
                        var removeChatResponse = ((JObject)container.Payload).ToObject(typeof(RemoveChatResponse)) as RemoveChatResponse;
                        RemovedChat?.Invoke(this, new RemovedChatEventArgs(removeChatResponse.NameClient, removeChatResponse.NumberChat));
                        break;
                    }
                case nameof(AddClientToChatResponse):
                    {
                        var addNewClientToChatResponse = ((JObject)container.Payload)
                                                    .ToObject(typeof(AddClientToChatResponse)) as AddClientToChatResponse;
                        AddedClientsToChat?.Invoke(this, new AddedClientsToChatEventArgs(addNewClientToChatResponse.ClientName,
                                                                                         addNewClientToChatResponse.NumberChat,
                                                                                         addNewClientToChatResponse.Clients));
                        break;
                    }
                case nameof(RemoveClientFromChatResponse):
                    {
                        var removeClientFromChatResponse = ((JObject)container.Payload)
                                                        .ToObject(typeof(RemoveClientFromChatResponse)) as RemoveClientFromChatResponse;
                        RemovedClientsFromChat?.Invoke(this, new RemovedClientsFromChatEventArgs(removeClientFromChatResponse.ClientName,
                                                                                             removeClientFromChatResponse.NumberChat,
                                                                                             removeClientFromChatResponse.Clients));
                        break;
                    }
                case nameof(NumbersAccessibleChatsResponse):
                    {
                        var responseNumbersChats = ((JObject)container.Payload)
                                                        .ToObject(typeof(NumbersAccessibleChatsResponse)) as NumbersAccessibleChatsResponse;
                        ResponseNumbersChats?.Invoke(this, new NumbersOfChatsReceivedModelEventArgs(responseNumbersChats.AllInfoAboutChat));
                        break;
                    }
                case nameof(InfoAboutAllClientsResponse):
                    {
                        var responseInfoAboutClients = ((JObject)container.Payload)
                                                        .ToObject(typeof(InfoAboutAllClientsResponse)) as InfoAboutAllClientsResponse;
                        ReceivedInfoAboutAllClients?.Invoke(this, new ReceivedInfoAboutAllClientsVmEventArgs(responseInfoAboutClients.InfoAboutAllClients));
                        break;
                    }
            }
        }

        #endregion Methods
    }
}
