using Common.Network;
using Common.Network.Packets;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Model
{
    public class HandlerResponseFromServer : IHandlerResponseFromServer
    {
        public event EventHandler<ClientConnectedToServerEventArgs> ClientConnected;
        public event EventHandler<AnotherClientConnectedEventArgs> AnotherClientConnected;
        public event EventHandler<MessageReceivedEventArgs> MessageReceived;
        public event EventHandler<ClientConnectedToChatEventArgs> ConnectedToChat;
        public event EventHandler<AddedNewChatModelEventArgs> AddedChat;
        public event EventHandler<AddedClientsToChatEventArgs> AddedClientsToChat;
        public event EventHandler<RemovedClientsFromChatEventArgs> RemovedClientsFromChat;
        public event EventHandler<ClientDisconnectedEventArgs> ClientDisconnected;
        public event EventHandler<NumbersOfChatsReceivedEventArgs> RequestNumbersChats;

        public void ParsePacket(MessageContainer container)
        {
            switch (container.Identifier)
            {
                case nameof(ConnectionResponse):
                    {
                        var connectionResponse = ((JObject)container.Payload).ToObject(typeof(ConnectionResponse)) as ConnectionResponse;
                        ClientConnected?.Invoke(this, new ClientConnectedToServerEventArgs(connectionResponse.Result, connectionResponse.Reason));
                        break;
                    }
                case nameof(ConnectionNoticeForClients):
                    {
                        var connectionNoticeForClients = ((JObject)container.Payload).ToObject(typeof(ConnectionNoticeForClients)) as ConnectionNoticeForClients;
                        AnotherClientConnected?.Invoke(this, new AnotherClientConnectedEventArgs(connectionNoticeForClients.NameOfClient));
                        break;
                    }
                case nameof(DisconnectNotice):
                    {
                        var disconnectionResponse = ((JObject)container.Payload).ToObject(typeof(DisconnectNotice)) as DisconnectNotice;
                        ClientDisconnected?.Invoke(this, new ClientDisconnectedEventArgs(disconnectionResponse.NameOfClient));
                        break;
                    }
                case nameof(MessageResponse):
                    {
                        var messageResponse = ((JObject)container.Payload).ToObject(typeof(MessageResponse)) as MessageResponse;
                        MessageReceived?.Invoke(this, new MessageReceivedEventArgs(messageResponse.FromClientName, messageResponse.Message, messageResponse.NumberChat));
                        break;
                    }
                case nameof(ConnectToChatResponse):
                    {
                        var connectionToChatResponse = ((JObject)container.Payload).ToObject(typeof(ConnectToChatResponse)) as ConnectToChatResponse;
                        ConnectedToChat?.Invoke(this, new ClientConnectedToChatEventArgs(connectionToChatResponse.AllMessageFromChat, connectionToChatResponse.NumberChat));
                        break;
                    }
                case nameof(AddNewChatResponse):
                    {
                        var addNewChatResponse = ((JObject)container.Payload).ToObject(typeof(AddNewChatResponse)) as AddNewChatResponse;
                        AddedChat?.Invoke(this, new AddedNewChatModelEventArgs(addNewChatResponse.NumberChat, addNewChatResponse.Clients));
                        break;
                    }
                case nameof(AddNewClientToChatResponse):
                    {
                        var addNewClientToChatResponse = ((JObject)container.Payload)
                                                    .ToObject(typeof(AddNewClientToChatResponse)) as AddNewClientToChatResponse;
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
                case nameof(GetNumbersAccessibleChatsResponse):
                    {
                        var responseNumbersChats = ((JObject)container.Payload)
                                                        .ToObject(typeof(GetNumbersAccessibleChatsResponse)) as GetNumbersAccessibleChatsResponse;
                        RequestNumbersChats?.Invoke(this, new NumbersOfChatsReceivedEventArgs(responseNumbersChats.NumbersChatsForClient, 
                                                                                              responseNumbersChats.ClientsAtChat));
                        break;
                    }
            }
        }
    }
}
