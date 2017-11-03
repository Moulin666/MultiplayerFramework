using MultiplayerGameFramework.Interfaces.Config;
using System.Collections.Generic;
using System.Linq;

namespace MultiplayerGameFramework.Implementation.Config
{
	public class PeerConfig : IPeerConfig
	{
		private List<object> _configs = new List<object>();

		public T GetConfig<T>() where T : class
		{
			return _configs.FirstOrDefault(c => c is T) as T;
		}

		public void AddConfig(object obj)
		{
			_configs.Add(obj);
		}
	}
}
