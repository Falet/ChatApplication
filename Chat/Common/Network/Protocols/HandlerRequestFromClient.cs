using Common.Network.Packets;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Network
{
    public class HandlerRequestFromClient : IHandlerRequestFromClient
    {
        public event EventHandler<ClientConnectedEventArgs> ClientConnected;
        public event EventHandler<MessageReceivedEventArgs> MessageReceived;
        public event EventHandler<ConnectionToChatEventArgs> ConnectedToChat;
        public event EventHandler<AddedNewChatEventArgs> AddedChat;
        public event EventHandler<RemovedChatEventArgs> RemovedChat;
        public event EventHandler<AddedClientsToChatEventArgs> AddedClientsToChat;
        public event EventHandler<RemovedClientsFromChatEventArgs> RemovedClientsFromChat;
        public event EventHandler<ClientDisconnectedEventArgs> ClientDisconnected;
        public event EventHandler<ClientRequestedNumbersChatEventArgs> RequestNumbersChats;
        public event EventHandler<InfoAboutAllClientsEventArgs> RequestInfoAllClient;
        public void ParsePacket(Guid clientId, MessageContainer container)
        {
            switch (container.Identifier)
            {
                case nameof(ConnectionRequest):
                    {
                        var connectionRequest = ((JObject)container.Payload).ToObject(typeof(ConnectionRequest)) as ConnectionRequest;
                        ClientConnected?.Invoke(this, new ClientConnectedEventArgs(connectionRequest.ClientName, clientId));
                        break;
                    }
                case nameof(DisconnectNotice):
                    {
                        var disconnectionRequest = ((JObject)container.Payload).ToObject(typeof(DisconnectNotice)) as DisconnectNotice;
                        ClientDisconnected?.Invoke(this, new ClientDisconnectedEventArgs(disconnectionRequest.NameOfClient,clientId));
                        break;
                    }
                case nameof(InfoAboutAllClientsRequest):
                    {
                        var infoClientRequest = ((JObject)container.Payload).ToObject(typeof(InfoAboutAllClientsRequest)) as InfoAboutAllClientsRequest;
                        RequestInfoAllClient?.Invoke(this, new InfoAboutAllClientsEventArgs(infoClientRequest.NameClient));
                        break;
                    }
                case nameof(MessageRequest):
                    {
                        var messageRequest = ((JObject)container.Payload).ToObject(typeof(MessageRequest)) as MessageRequest;
                        MessageReceived?.Invoke(this, new MessageReceivedEventArgs(messageRequest.ClientName, messageRequest.Message, messageRequest.NumberChat));
                        break;
                    }
                case nameof(ConnectToChatRequest):
                    {
                        var connectionToChat = ((JObject)container.Payload).ToObject(typeof(ConnectToChatRequest)) as ConnectToChatRequest;
                        ConnectedToChat?.Invoke(this, new ConnectionToChatEventArgs(connectionToChat.ClientName, connectionToChat.NumberChat));
                        break;
                    }
                case nameof(AddNewChatRequest):
                    {
                        var addNewChatRequest = ((JObject)container.Payload).ToObject(typeof(AddNewChatRequest)) as AddNewChatRequest;
                        AddedChat?.Invoke(this, new AddedNewChatEventArgs(addNewChatRequest.NameClientSender, addNewChatRequest.Clients));
                        break;
                    }
                case nameof(RemoveChatRequest):
                    {
                        var removeChatRequest = ((JObject)container.Payload).ToObject(typeof(RemoveChatRequest)) as RemoveChatRequest;
                        RemovedChat?.Invoke(this, new RemovedChatEventArgs(removeChatRequest.NameOfRemover, removeChatRequest.NumberChat));
                        break;
                    }
                case nameof(AddNewClientToChatRequest):
                    {
                        var addNewClientToChatRequest = ((JObject)container.Payload)
                                                    .ToObject(typeof(AddNewClientToChatRequest)) as AddNewClientToChatRequest;
                        AddedClientsToChat?.Invoke(this, new AddedClientsToChatEventArgs(addNewClientToChatRequest.ClientName,
                                                                                     addNewClientToChatRequest.NumberChat,
                                                                                     addNewClientToChatRequest.Clients));
                        break;
                    }
                case nameof(RemoveClientFromChatRequest):
                    {
                        var removeClientFromChatRequest = ((JObject)container.Payload)
                                                        .ToObject(typeof(RemoveClientFromChatRequest)) as RemoveClientFromChatRequest;
                        RemovedClientsFromChat?.Invoke(this, new RemovedClientsFromChatEventArgs(removeClientFromChatRequest.ClientName,
                                                                                             removeClientFromChatRequest.NumberChat,
                                                                                             removeClientFromChatRequest.Clients));
                        break;
                    }
                case nameof(GetNumbersAccessibleChatsRequest):
                    {
                        var requestNumbersChats = ((JObject)container.Payload)
                                                        .ToObject(typeof(GetNumbersAccessibleChatsRequest)) as GetNumbersAccessibleChatsRequest;
                        RequestNumbersChats?.Invoke(this, new ClientRequestedNumbersChatEventArgs(requestNumbersChats.NameClient));
                        break;
                    }
            }
        }
    }
}
