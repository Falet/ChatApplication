using System;
using System.Collections.Generic;
using System.Text;

namespace TestServer.Network
{
	public class UserProperties
	{
		#region Properties

		public Guid IdConnection { get; set; }

		public List<InfoChat> Room { get; set; }

		#endregion Properties
	}
}
