﻿namespace Common.Network.Packets
{
    using System.Collections.Generic;
    public class AddNewClientToChatRequest
    {
        #region Properties

        public string ClientName { get; }

        public List<string> Clients { get; }

        public int NumberChat { get; }

        #endregion Properties

        #region Constructors

        public AddNewClientToChatRequest(string clientName, List<string> clients, int numberChat)
        {
            ClientName = clientName;
            Clients = clients;
            NumberChat = numberChat;
        }

        #endregion Constructors

        #region Methods

        public MessageContainer GetContainer()
        {
            var container = new MessageContainer
            {
                Identifier = nameof(AddNewClientToChatRequest),
                Payload = this
            };

            return container;
        }

        #endregion Methods

    }
}