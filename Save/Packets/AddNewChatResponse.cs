using System;
using System.Collections.Generic;
using System.Text;

namespace TestServer.Network
{
    public class AddNewChatResponse
    {
        public int NumberRoom { get; }
        public List<string> Users { get; }

        #region Constructors

        public AddNewChatResponse(int numberRoom, List<string> users)
        {
            NumberRoom = numberRoom;
            Users = users;
        }

        #endregion Constructors

        #region Methods

        public MessageContainer GetContainer()
        {
            var container = new MessageContainer
            {
                Identifier = nameof(AddNewChatResponse),
                Payload = this
            };

            return container;
        }

        #endregion Methods
    }
}
