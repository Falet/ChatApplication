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
			handler.NewUserConnected += OnNewMessage;
		}
		public void OnNewUser(object sender, ConnectionStateChangedEventArgs newUser)
        {

        }
		public void OnNewMessage(object sender, ConnectionStateChangedEventArgs newUser)
		{

		}
	}
}
