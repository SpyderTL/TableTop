using System.Collections.Generic;

namespace Table
{
	public class Player
	{
		public string Name;
		public string Token;
		public Queue<string> Outbox = new Queue<string>();
	}
}