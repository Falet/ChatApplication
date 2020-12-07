using System;
using System.Collections.Generic;
using System.Text;

namespace TestServer.Network
{
    public class ConnectToChatResponse
    {
        #region Properties

        public int Room { get; }
        public List<MessageInfo> AllMessageFromChat { get; }

        #endregion Properties

        #region Constructors

        public ConnectToChatResponse(int room, List<MessageInfo> allMessageFromChat)
        {
            Room = room;
            AllMessageFromChat = allMessageFromChat;
        }

        #endregion Constructors

        #region Methods

        public MessageContainer GetContainer()
        {
            var container = new MessageContainer
            {
                Identifier = nameof(ConnectToChatResponse),
                Payload = this
            };

            return container;
        }

        #endregion Methods
    }
}
