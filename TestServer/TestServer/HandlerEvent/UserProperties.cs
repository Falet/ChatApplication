using System;
using System.Collections.Generic;
using System.Text;

namespace TestServer.Network
{
	public class UserProperties
	{
		#region Properties

		public string Login { get; set; }

		public Guid IdConnection { get; set; }

		public List<int> Room { get; set; }

		public string OwnerRoom { get; set; }

		#endregion Properties
	}
}
