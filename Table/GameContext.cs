using System;
using System.Collections.Generic;
using System.Threading;

namespace Table
{
	public class GameContext
	{
		public static GameContext Current = new GameContext();

		public List<Character> Characters = new List<Character>();
		public Thread Thread;
		public bool Stopping;

		public void Start()
		{
			Thread = new Thread(Thread_Start);
			Thread.Start();
		}

		private void Thread_Start()
		{
			while (!Stopping)
			{
				//SessionContext.Current.Players.ForEach(x => x.Outbox.Enqueue("Hi There!"));
				Thread.Sleep(100);
			}
		}

		public void Stop()
		{
			Stopping = true;

			Thread.Join(5000);
		}
	}
}