﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace TestServer.Network
{
    using WebSocketSharp;
    using WebSocketSharp.Server;
    using Newtonsoft.Json;
    using System.Threading;
    using System.Collections.Concurrent;

    public class WsConnection : WebSocketBehavior
    {
        #region Fields

        private WsServer _server;

        private readonly ConcurrentQueue<MessageContainer> _sendQueue;

        private int _sending;

        #endregion Fields

        #region Properties

        public Guid Id { get; set; }

        public string Login { get; set; }

        public bool IsConnected => Context.WebSocket?.ReadyState == WebSocketState.Open;

        #endregion Properties

        public WsConnection()
        {
            _sendQueue = new ConcurrentQueue<MessageContainer>();
            _sending = 0;

            Id = Guid.NewGuid();
        }

        #region Methods

        public void AddServer(WsServer server)
        {
            _server = server;
        }

        public void Send(MessageContainer container)
        {
            if (!IsConnected)
                return;

            _sendQueue.Enqueue(container);
            if (Interlocked.CompareExchange(ref _sending, 1, 0) == 0)
                SendImpl();
        }
        public void Close()
        {
            Context.WebSocket.Close();
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
            }
            
        }
        private void SendCompleted(bool completed)
        {
            // При отправке произошла ошибка.
            if (!completed)
            {
                _server.FreeConnection(Id);
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
