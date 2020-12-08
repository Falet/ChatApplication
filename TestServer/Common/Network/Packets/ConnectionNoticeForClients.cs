using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Network.Packets
{
    public class ConnectionNoticeForClients
    {
        #region Properties

        public string NameOfClient { get; }

        public List<int> NumberChat { get; }

        #endregion Properties

        #region Constructors

        public ConnectionNoticeForClients(string nameOfClient, List<int> numberChat)
        {
            NameOfClient = nameOfClient;
            NumberChat = numberChat;
        }

        #endregion Constructors

        #region Methods

        public MessageContainer GetContainer()
        {
            var container = new MessageContainer
            {
                Identifier = nameof(ConnectionNoticeForClients),
                Payload = this
            };

            return container;
        }

        #endregion Methods
    }
}
