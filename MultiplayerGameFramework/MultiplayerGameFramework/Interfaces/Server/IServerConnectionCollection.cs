using MultiplayerGameFramework.Interfaces.Client;
using System.Collections.Generic;

namespace MultiplayerGameFramework.Interfaces.Server
{
	public interface IServerConnectionCollection<TServerType, TPeer> : IConnectionCollection<TPeer>
	{
		Dictionary<TServerType, List<TPeer>> GetServers();
		List<T> GetServersByType<T>(TServerType type) where T : class, TPeer;
	}
}
