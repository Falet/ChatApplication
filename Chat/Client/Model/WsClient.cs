﻿using Common.Network.Packets;
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

namespace Client.Model
{
    public class WsClient: ITransportClient
    {
        #region Fields

        private readonly ConcurrentQueue<MessageContainer> _sendQueue;
        private IHandlerResponseFromServer _handlerResponseFromServer;
        private int _sending;
        private WebSocket _socket;

        #endregion Fields

        public bool IsConnected => _socket?.ReadyState == WebSocketState.Open;

        #region Constructors

        public WsClient()
        {
            _sendQueue = new ConcurrentQueue<MessageContainer>();
            _sending = 0;
            _socket = new WebSocket($"ws://192.168.37.106:35");
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
            _socket.OnOpen += OnOpen;
            _socket.OnClose += OnClose;
            _socket.OnMessage += OnMessage;
            _socket.Connect();
        }
        public void Send(MessageContainer container)
        {
            var settings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
            string serializedMessages = JsonConvert.SerializeObject(container, settings);
            _socket.Send(serializedMessages);
            /*_sendQueue.Enqueue(container);
            SendImpl();*/
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
        private void SendCompleted(bool completed)
        {
            // При отправке произошла ошибка.
            if (!completed)
            {
                _socket.CloseAsync();
                return;
            }
            SendImpl();
        }

        private void SendImpl()
        {
            if (!IsConnected)
                return;

            if (!_sendQueue.TryDequeue(out var message))
                return;

            var settings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
            string serializedMessages = JsonConvert.SerializeObject(message, settings);
            _socket.Send(serializedMessages);
        }
        #endregion Methods
    }

}
