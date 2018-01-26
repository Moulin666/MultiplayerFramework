using MultiplayerGameFramework.Interfaces.Config;

namespace MultiplayerGameFramework.Interfaces.Support
{
	/// <summary>
	/// Factory of peer.
	/// </summary>
	public interface IPeerFactory
	{
		/// <summary>
		/// Create new peer.
		/// </summary>
		/// <typeparam name="T">class.</typeparam>
		/// <param name="config">Peer configuration.</param>
		/// <returns>T peer class.</returns>
		T CreatePeer<T>(IPeerConfig config) where T : class;
	}
}
