using Common.Network;
using Common.Network.Packets;
using Newtonsoft.Json;
namespace Client.Model
{
    using Client.Model.Event;
    using System;
    using System.Collections.Generic;

    class HandlerConnection : IHandlerConnection
    {
        #region Fields

        private ITransportClient _transportClient;
        private IClientInfo _clientInfo;

        #endregion Fields

        #region Properties

        public Dictionary<string, bool> InfoClientsAtChat { get; private set; }

        #endregion Properties

        #region Event

        public event EventHandler<ClientConnectedToServerVmEventArgs> ClientConnected;
        public event EventHandler<AnotherClientConnectedVmEventArgs> AnotherClientConnected;
        public event EventHandler<AnotherClientConnectedVmEventArgs> AnotherNewClientConnected;
        public event EventHandler<AnotherClientDisconnectedVmEventArgs> AnotherClientDisconnected;
        public event EventHandler<ReceivedInfoAboutAllClientsVmEventArgs> ReceivedInfoAboutAllClients;

        #endregion Event

        #region Constructors

        public HandlerConnection(IClientInfo clientInfo, ITransportClient transportClient, IHandlerResponseFromServer handlerResponseFromServer)
        {
            _clientInfo = clientInfo;
            _transportClient = transportClient;
            handlerResponseFromServer.ClientConnected += OnClientConnected;
            handlerResponseFromServer.AnotherClientConnected += OnAnotherClientConnected;
            handlerResponseFromServer.AnotherClientDisconnected += OnAnotherClientDisconnected;
            handlerResponseFromServer.ReceivedInfoAboutAllClients += OnReceivedInfoAboutAllClients;

            InfoClientsAtChat = new Dictionary<string, bool>();
        }

        #endregion Constructors

        #region Methods

        public void Connect(string ip, string port, string protocol)
        {
            _transportClient.Connect(ip, int.Parse(port));
        }

        public void Send(string login)
        {
            _clientInfo.Login = login;
            string serializedMessages = JsonConvert.SerializeObject(Container.GetContainer(nameof(ConnectionRequest), new ConnectionRequest(login)));
            _transportClient.Send(Container.GetContainer(nameof(ConnectionRequest), new ConnectionRequest(login)));
        }

        private void OnClientConnected(object sender, ClientConnectedToServerVmEventArgs container)
        {
            if (container.Result == ResultRequest.Ok)
            {
                _transportClient.Send(Container.GetContainer(nameof(InfoAboutAllClientsRequest), new InfoAboutAllClientsRequest(_clientInfo.Login)));
                ClientConnected?.Invoke(this, new ClientConnectedToServerVmEventArgs(container.Result, container.Reason));
            }
            else
            {
                ClientConnected?.Invoke(this, new ClientConnectedToServerVmEventArgs(container.Result, container.Reason));
            }
        }
        private void OnAnotherClientDisconnected(object sender, AnotherClientDisconnectedVmEventArgs container)
        {
            if (InfoClientsAtChat.TryGetValue(container.NameClient, out bool activityClient) && activityClient == true)
            {
                InfoClientsAtChat[container.NameClient] = false;
            }
            AnotherClientDisconnected?.Invoke(this, new AnotherClientDisconnectedVmEventArgs(container.NameClient));
        }
        private void OnAnotherClientConnected(object sender, AnotherClientConnectedVmEventArgs container)
        {
            if (InfoClientsAtChat.TryGetValue(container.NameClient, out bool activityClient))
            {
                if (activityClient == false)
                {
                    InfoClientsAtChat[container.NameClient] = true;
                }
                AnotherClientConnected?.Invoke(this, new AnotherClientConnectedVmEventArgs(container.NameClient));
            }
            else
            {
                InfoClientsAtChat.Add(container.NameClient, true);
                AnotherNewClientConnected?.Invoke(this, new AnotherClientConnectedVmEventArgs(container.NameClient));
            }

        }
        private void OnReceivedInfoAboutAllClients(object sender, ReceivedInfoAboutAllClientsVmEventArgs container)
        {
            InfoClientsAtChat = container.InfoClientsAtChat;
            ReceivedInfoAboutAllClients?.Invoke(this, new ReceivedInfoAboutAllClientsVmEventArgs(container.InfoClientsAtChat));
        }

        #endregion Methods
    }
}
