using Common.Network.Packets;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace Client.Model
{
    public class WsClient: WebSocketBehavior, ITransportClient
    {
        #region Fields

        private readonly ConcurrentQueue<MessageContainer> _sendQueue;
        private IHandlerResponseFromServer _handlerResponseFromServer;
        private int _sending;
        private WebSocket _socket;

        #endregion Fields

        #region Properties

        public bool IsConnected => Context.WebSocket?.ReadyState == WebSocketState.Open;

        #endregion Properties

        #region Constructors

        public WsClient()
        {
            _sendQueue = new ConcurrentQueue<MessageContainer>();
            _sending = 0;
        }

        #endregion Constructors

        #region Methods

        public void AddHandlerMessage(IHandlerResponseFromServer handlerResponseFromServer)
        {
            _handlerResponseFromServer = handlerResponseFromServer;
        }
        public void Connect(string ip, int port)
        {
            _socket = new WebSocket($"ws://{ip}:{port}");
        }
        public void Send(MessageContainer container)
        {
            if (!IsConnected)
                return;

            _sendQueue.Enqueue(container);
            if (Interlocked.CompareExchange(ref _sending, 1, 0) == 0)
                SendImpl();
        }
        protected override void OnOpen()
        {
            
        }

        protected override void OnClose(CloseEventArgs e)
        {
            
        }

        protected override void OnMessage(MessageEventArgs e)
        {
            if (e.IsText)
            {
                var message = JsonConvert.DeserializeObject<MessageContainer>(e.Data);
                _handlerResponseFromServer.ParsePacket(message);
            }
        }
        private void SendCompleted(bool completed)
        {
            // При отправке произошла ошибка.
            if (!completed)
            {
                Context.WebSocket.CloseAsync();
                return;
            }

            SendImpl();
        }

        private void SendImpl()
        {
            if (!IsConnected)
                return;

            if (!_sendQueue.TryDequeue(out var message) && Interlocked.CompareExchange(ref _sending, 0, 1) == 1)
                return;

            var settings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
            string serializedMessages = JsonConvert.SerializeObject(message, settings);
            SendAsync(serializedMessages, SendCompleted);
        }


        #endregion Methods
    }

}
