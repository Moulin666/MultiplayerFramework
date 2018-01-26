using MultiplayerGameFramework.Interfaces.Client;
using System.Collections.Generic;

namespace MultiplayerGameFramework.Interfaces.Server
{
	/// <summary>
	/// Server connection collection.
	/// </summary>
	/// <typeparam name="TServerType">Server type.</typeparam>
	/// <typeparam name="TPeer">Client.</typeparam>
	public interface IServerConnectionCollection<TServerType, TPeer> : IConnectionCollection<TPeer>
	{
		/// <summary>
		/// Get dictionary with servers. Where key is TServerType, parameter is List with TPeer.
		/// </summary>
		/// <returns></returns>
		Dictionary<TServerType, List<TPeer>> GetServers();
		/// <summary>
		/// Get servers by ServerType.
		/// </summary>
		/// <typeparam name="T">class, TPeer.</typeparam>
		/// <param name="type">Server Type.</param>
		/// <returns></returns>
		List<T> GetServersByType<T>(TServerType type) where T : class, TPeer;
	}
}
