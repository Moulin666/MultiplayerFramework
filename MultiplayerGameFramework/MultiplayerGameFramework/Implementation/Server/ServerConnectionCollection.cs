using MultiplayerGameFramework.Interfaces.Config;
using MultiplayerGameFramework.Interfaces.Server;
using System.Collections.Generic;
using System.Linq;

namespace MultiplayerGameFramework.Implementation.Server
{
	/// <summary>
	/// Server connection collection.
	/// </summary>
	/// <typeparam name="TServerType">Server type.</typeparam>
	/// <typeparam name="TPeer">Client.</typeparam>
	public class ServerConnectionCollection : IServerConnectionCollection<IServerType, IServerPeer>
	{
		private List<IServerPeer> _servers;

		/// <summary>
		/// Base constructor.
		/// </summary>
		public ServerConnectionCollection()
		{
			_servers = new List<IServerPeer>();
		}

		/// <summary>
		/// Connect new server.
		/// </summary>
		/// <param name="peer">Server</param>
		public void Connect(IServerPeer peer)
		{
			_servers.Add(peer);
		}

		/// <summary>
		/// Desconnect server.
		/// </summary>
		/// <param name="peer">Server.</param>
		public void Disconnect(IServerPeer peer)
		{
			_servers.Remove(peer);
			peer.Disconnect();
		}

		/// <summary>
		/// Clear all servers.
		/// </summary>
		public void Clear()
		{
			_servers.Clear();
		}

		/// <summary>
		/// Get list all server.
		/// </summary>
		/// <typeparam name="T">class, IServerPeer</typeparam>
		/// <returns>List T</returns>
		public List<T> GetPeers<T>() where T : class, IServerPeer
		{
			return new List<T>(_servers.Cast<T>());
		}

		/// <summary>
		/// Get dictionary with servers. Where key is TServerType, parameter is List with TPeer.
		/// </summary>
		/// <returns></returns>
		public Dictionary<IServerType, List<IServerPeer>> GetServers()
		{
			var retValue = new Dictionary<IServerType, List<IServerPeer>>();

			foreach(IServerPeer server in _servers)
			{
				if(server.ServerType != null)
				{
					if(retValue.ContainsKey(server.ServerType))
					{
						retValue.Add(server.ServerType, new List<IServerPeer>());
					}

					retValue[server.ServerType].Add(server);
				}
			}

			return retValue;
		}

		/// <summary>
		/// Get servers by ServerType.
		/// </summary>
		/// <typeparam name="T">class, TPeer.</typeparam>
		/// <param name="type">Server Type.</param>
		/// <returns></returns>
		public List<T> GetServersByType<T>(IServerType type) where T : class, IServerPeer
		{
			var retValue = new List<T>();

			foreach(var server in _servers)
			{
				if(server.ServerType == type && server is T)
				{
					retValue.Add(server as T);
				}
			}

			return retValue;
		}
	}
}
