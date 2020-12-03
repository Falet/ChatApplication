using System;
using System.Collections.Generic;
using System.Text;

namespace TestServer.Network
{
	public class ChangeDb
	{
		public ChangeDb(HandlerRequestFromServer handler, RequestManagerDb requestManagerDb)
        {
			handler.NewUserConnected += OnNewUser;
			handler.NewMessageRecieved += OnNewMessage;
			handler.NewUsersAddedToChat += OnAddedUserToChat;
			handler.UsersFromChatRemoved += OnRemovedUsersFromChat;
			handler.NewChatCreated += OnCreatedChar;
			handler.ChatRemoved += OnRemovedChat;
		}
		public void OnNewUser(object sender, UserConnectedEventArgs newUser)
        {

        }
		public void OnNewMessage(object sender, MessageReceivedEventArgs newUser)
		{

		}
		public void OnAddedUserToChat(object sender, AddedUsersToChatEventArgs newUser)
		{

		}
		public void OnRemovedUsersFromChat(object sender, RemovedUsersFromChatEventArgs newUser)
		{

		}
		public void OnCreatedChar(object sender, AddedChatEventArgs newUser)
		{

		}
		public void OnRemovedChat(object sender, RemovedChatEventArgs newUser)
		{

		}
	}
}
