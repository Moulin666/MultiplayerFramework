using MultiplayerGameFramework.Interfaces.Client;

namespace MultiplayerGameFramework.Interfaces.Support
{
	public interface IClientConnectionHandler
	{
		void ClientConnect(IClientPeer peer);
		void ClientDisconnect(IClientPeer peer);
	}
}
