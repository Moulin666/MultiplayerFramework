using System.Collections.Generic;

namespace MultiplayerGameFramework.Interfaces.Client
{
	/// <summary>
	/// Connection collection. Store all connection.
	/// </summary>
	/// <typeparam name="TPeer">Connected peer.</typeparam>
	public interface IConnectionCollection<TPeer>
	{
		/// <summary>
		/// Connect to the server.
		/// </summary>
		/// <param name="peer">TPeer.</param>
		void Connect(TPeer peer);
		/// <summary>
		/// Disconnect from the server.
		/// </summary>
		/// <param name="peer">TPeer.</param>
		void Disconnect(TPeer peer);
		/// <summary>
		/// Clear collection.
		/// </summary>
		void Clear();

		/// <summary>
		/// Get list of peers.
		/// </summary>
		/// <typeparam name="T">class, TPeer.</typeparam>
		/// <returns>List<T> Peers.</returns>
		List<T> GetPeers<T>() where T : class, TPeer;
	}
}
