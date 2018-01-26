using MultiplayerGameFramework.Interfaces.Config;
using System.Collections.Generic;
using System.Linq;

namespace MultiplayerGameFramework.Implementation.Config
{
	/// <summary>
	/// Peer configuration.
	/// </summary>
	public class PeerConfig : IPeerConfig
	{
		private List<object> _configs = new List<object>();

		/// <summary>
		/// Get Configuration.
		/// </summary>
		/// <typeparam name="T">class.</typeparam>
		/// <returns>T configuration.</returns>
		public T GetConfig<T>() where T : class
		{
			return _configs.FirstOrDefault(c => c is T) as T;
		}

		/// <summary>
		/// Add new configuration.
		/// </summary>
		/// <param name="obj">Configuration for adding.</param>
		public void AddConfig(object obj)
		{
			_configs.Add(obj);
		}
	}
}
