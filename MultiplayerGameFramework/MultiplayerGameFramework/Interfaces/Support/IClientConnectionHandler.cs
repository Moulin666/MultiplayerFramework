using MultiplayerGameFramework.Interfaces.Client;

namespace MultiplayerGameFramework.Interfaces.Support
{
	/// <summary>
	/// Client connection handler.
	/// </summary>
	public interface IClientConnectionHandler
	{
		/// <summary>
		/// Client connect handler.
		/// </summary>
		/// <param name="peer">Client.</param>
		void ClientConnect(IClientPeer peer);
		/// <summary>
		/// Client disconnect handler.
		/// </summary>
		/// <param name="peer">Client.</param>
		void ClientDisconnect(IClientPeer peer);
	}
}
