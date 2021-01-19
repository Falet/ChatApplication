using Common.Network;
using Common.Network.Packets;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Client.Model
{
    class HandlerConnection : IHandlerConnection
    {
        private ITransportClient _transportClient;
        private IClientInfo _clientInfo;

        public Dictionary<string, bool> InfoClientsAtChat { get; private set; }
        public event EventHandler<AnotherClientDisconnectedEventArgs> AnotherClientDisconnected;
        public event EventHandler<ClientConnectedToServerEventArgs> ClientConnected;
        public event EventHandler<ReceivedInfoAboutAllClientsEventArgs> ReceivedInfoAboutAllClients;
        public event EventHandler<AnotherClientConnectedEventArgs> AnotherClientConnected;
        public event EventHandler<AnotherClientConnectedEventArgs> AnotherNewClientConnected;

        public HandlerConnection(IClientInfo clientInfo, ITransportClient transportClient, IHandlerResponseFromServer handlerResponseFromServer)
        {
            _clientInfo = clientInfo;
            _transportClient = transportClient;
            handlerResponseFromServer.ClientConnected += OnClientConnected;
            handlerResponseFromServer.AnotherClientDisconnected += OnAnotherClientDisconnected;
            handlerResponseFromServer.AnotherClientConnected += OnAnotherClientConnected;
            handlerResponseFromServer.ReceivedInfoAboutAllClients += OnReceivedInfoAboutAllClients;

            InfoClientsAtChat = new Dictionary<string, bool>();
        }
        public void Connect(string ip, string port, string protocol)
        {
            _transportClient.Connect(ip,int.Parse(port));
        }

        public void Send(string login)
        {
            _clientInfo.Login = login;
            string serializedMessages = JsonConvert.SerializeObject(Container.GetContainer(nameof(ConnectionRequest), new ConnectionRequest(login)));
            _transportClient.Send(Container.GetContainer(nameof(ConnectionRequest),new ConnectionRequest(login)));
        }

        private void OnClientConnected(object sender, ClientConnectedToServerEventArgs container)
        {
            if(container.Result == ResultRequest.Ok)
            {
                _transportClient.Send(Container.GetContainer(nameof(InfoAboutAllClientsRequest), new InfoAboutAllClientsRequest(_clientInfo.Login)));
                ClientConnected?.Invoke(this, new ClientConnectedToServerEventArgs(container.Result, container.Reason));
            }
            else
            {
                ClientConnected?.Invoke(this, new ClientConnectedToServerEventArgs(container.Result, container.Reason));
            }
        }
        private void OnAnotherClientDisconnected(object sender, AnotherClientDisconnectedEventArgs container)
        {
            if (InfoClientsAtChat.TryGetValue(container.NameClient, out bool activityClient) && activityClient == true)
            {
                InfoClientsAtChat[container.NameClient] = false;
            }
            AnotherClientDisconnected?.Invoke(this, new AnotherClientDisconnectedEventArgs(container.NameClient));
        }
        private void OnAnotherClientConnected(object sender, AnotherClientConnectedEventArgs container)
        {
            if(InfoClientsAtChat.TryGetValue(container.NameClient, out bool activityClient))
            {
                if(activityClient == false)
                {
                    InfoClientsAtChat[container.NameClient] = true;
                }
                AnotherClientConnected?.Invoke(this, new AnotherClientConnectedEventArgs(container.NameClient));
            }
            else
            {
                InfoClientsAtChat.Add(container.NameClient, true);
                AnotherNewClientConnected?.Invoke(this, new AnotherClientConnectedEventArgs(container.NameClient));
            }
            
        }
        private void OnReceivedInfoAboutAllClients(object sender, ReceivedInfoAboutAllClientsEventArgs container)
        {
            InfoClientsAtChat = container.InfoClientsAtChat;
            ReceivedInfoAboutAllClients?.Invoke(this, new ReceivedInfoAboutAllClientsEventArgs(container.InfoClientsAtChat));
        }
    }
}
