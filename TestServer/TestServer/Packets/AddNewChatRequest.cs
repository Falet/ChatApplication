using System;
using System.Collections.Generic;
using System.Text;

namespace TestServer.Network
{
    public class AddNewChatRequest
    {
        public List<string> Users { get; }

        #region Constructors

        public AddNewChatRequest(List<string> users)
        {
            Users = users;
        }

        #endregion Constructors

        #region Methods

        public MessageContainer GetContainer()
        {
            var container = new MessageContainer
            {
                Identifier = nameof(AddNewChatRequest),
                Payload = this
            };

            return container;
        }

        #endregion Methods
    }
}
