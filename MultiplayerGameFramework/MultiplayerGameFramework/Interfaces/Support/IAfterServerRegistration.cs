using MultiplayerGameFramework.Interfaces.Server;

namespace MultiplayerGameFramework.Interfaces.Support
{
	public interface IAfterServerRegistration
	{
		void AfterRegister(IServerPeer serverPeer);
	}
}
