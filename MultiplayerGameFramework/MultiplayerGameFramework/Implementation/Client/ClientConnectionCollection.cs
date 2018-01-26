using MultiplayerGameFramework.Interfaces.Client;
using MultiplayerGameFramework.Interfaces.Support;
using System.Collections.Generic;
using System.Linq;

namespace MultiplayerGameFramework.Implementation.Client
{
	/// <summary>
	/// Store all client connection.
	/// </summary>
	public class ClientConnectionCollection : IConnectionCollection<IClientPeer>
	{
		private readonly IEnumerable<IClientConnectionHandler> _clientConnectionHandlers;
		private List<IClientPeer> _clients;

		/// <summary>
		/// Base constructor.
		/// </summary>
		/// <param name="clientConnectionHandlers">Connection handler.</param>
		public ClientConnectionCollection(IEnumerable<IClientConnectionHandler> clientConnectionHandlers)
		{
			_clientConnectionHandlers = clientConnectionHandlers;
			_clients = new List<IClientPeer>();
		}

		/// <summary>
		/// Connect to the server.
		/// </summary>
		/// <param name="peer">TPeer.</param>
		public void Connect(IClientPeer peer)
		{
			_clients.Add(peer);

			foreach(var clientConnectionHandler in _clientConnectionHandlers)
			{
				clientConnectionHandler.ClientConnect(peer);
			}
		}

		/// <summary>
		/// Clear collection.
		/// </summary>
		public void Clear()
		{
			_clients.Clear();
		}

		/// <summary>
		/// Disconnect from the server.
		/// </summary>
		/// <param name="peer">TPeer.</param>
		public void Disconnect(IClientPeer peer)
		{
			_clients.Remove(peer);

			foreach (var clientConnectionHandler in _clientConnectionHandlers)
			{
				clientConnectionHandler.ClientDisconnect(peer);
			}

			peer.Disconnect();
		}

		/// <summary>
		/// Get list of peers.
		/// </summary>
		/// <typeparam name="T">class, TPeer.</typeparam>
		/// <returns>List<T> Peers.</returns>
		public List<T> GetPeers<T>() where T : class, IClientPeer
		{
			return new List<T>(_clients.Cast<T>());
		}
	}
}
