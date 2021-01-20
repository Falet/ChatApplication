namespace Client.Model.Event
{
    using System.Collections.Generic;

    public class AddedClientsToChatClientVmEvenArgs
    {
        #region Properties

        public Dictionary<string, bool> NameOfClients { get; }

        public int NumberChat { get; }

        #endregion Properties

        #region Constructors

        public AddedClientsToChatClientVmEvenArgs(int numberChat, Dictionary<string, bool> nameOfClients)
        {
            NameOfClients = nameOfClients;
            NumberChat = numberChat;
        }

        #endregion Constructors
    }
}
