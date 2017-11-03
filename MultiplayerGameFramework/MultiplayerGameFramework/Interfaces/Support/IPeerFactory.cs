using MultiplayerGameFramework.Interfaces.Config;

namespace MultiplayerGameFramework.Interfaces.Support
{
	public interface IPeerFactory
	{
		T CreatePeer<T>(IPeerConfig config) where T : class;
	}
}
