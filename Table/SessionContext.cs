using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Table
{
	public class SessionContext
	{
		public static SessionContext Current = new SessionContext();

		public List<Player> Players = new List<Player>();
	}
}