using ExitGames.Logging;
using MultiplayerGameFramework;
using MultiplayerGameFramework.Implementation.Messaging;
using MultiplayerGameFramework.Interfaces.Client;
using MultiplayerGameFramework.Interfaces.Support;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Servers.BackgroundThreads
{
	public class TestBackgroundThread : IBackgroundThread
	{
		public ILogger Log { get; set; }
		public IConnectionCollection<IClientPeer> ConnectionCollection;

		private bool isRunning;

		/// <summary>
		/// NOTICE: Include all IoC objects this thread needs i.e. IRegion, IStats, etc...
		/// </summary>
		public TestBackgroundThread(IConnectionCollection<IClientPeer> connectionCollection, ILogger log)
		{
			Log = log;
			ConnectionCollection = connectionCollection;
		}

		public void Run(object threadContext)
		{
			Stopwatch timer = new Stopwatch();
			timer.Start();
			isRunning = true;
			
			while(isRunning)
			{
				try
				{
					if (ConnectionCollection.GetPeers<IClientPeer>().Count <= 0)
					{
						Thread.Sleep(5000);
						timer.Restart();
						continue;
					}

					if (timer.Elapsed < TimeSpan.FromMilliseconds(500))
					{
						if (500 - timer.ElapsedMilliseconds > 0)
						{
							Thread.Sleep(500 - (int)timer.ElapsedMilliseconds);
						}
						continue;
					}

					Update(timer.Elapsed);
					timer.Restart();
				}
				catch (Exception ex)
				{
					Log.ErrorFormat("[TestBackgroundThread(Run)]: Exception - {0}", ex.StackTrace);
				}
			}
		}

		public void Setup(IServerApplication server)
		{
		}

		public void Stop()
		{
			isRunning = false;
		}

		public void Update(TimeSpan elapsed)
		{
			Parallel.ForEach(ConnectionCollection.GetPeers<IClientPeer>(), SendUpdate);
		}

		public void SendUpdate(IClientPeer instance)
		{
			if (instance != null)
			{
				Log.DebugFormat("Send message to peer");
				instance.SendMessage(new Event(2, 3, new Dictionary<byte, object>()));
			}
		}
	}
}
