namespace Client.Model
{
    using Common.Network.Packets;
    using Newtonsoft.Json;
    using System;
    using WebSocketSharp;
    public class WsClient: ITransportClient
    {
        #region Fields

        private IHandlerResponseFromServer _handlerResponseFromServer;
        private WebSocket _socket;

        #endregion Fields


        #region Properties

        public bool IsConnected => _socket?.ReadyState == WebSocketState.Open;

        #endregion Properties

        #region Constructors

        public WsClient(IHandlerResponseFromServer handlerResponseFromServer)
        {
            _handlerResponseFromServer = handlerResponseFromServer;
        }

        #endregion Constructors

        #region Methods

        public void Connect(string ip, int port)
        {
            _socket = new WebSocket($"ws://{ip}:{port}");
            _socket.OnOpen += OnOpen;
            _socket.OnClose += OnClose;
            _socket.OnMessage += OnMessage;
            _socket.ConnectAsync();

        }
        public void Send(MessageContainer container)
        {
            if (!IsConnected)
            {
                return;
            }
            var settings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
            string serializedMessages = JsonConvert.SerializeObject(container, settings);
            _socket.Send(serializedMessages);
        }
        protected void OnOpen(object sender, EventArgs e)
        {
            
        }

        protected void OnClose(object sender, CloseEventArgs e)
        {
            
        }

        protected void OnMessage(object sender, MessageEventArgs e)
        {
            if (e.IsText)
            {
                var message = JsonConvert.DeserializeObject<MessageContainer>(e.Data);
                _handlerResponseFromServer.ParsePacket(message);
            }
        }
        #endregion Methods
    }
}
