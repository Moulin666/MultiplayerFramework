using MultiplayerGameFramework.Interfaces.Server;

namespace MultiplayerGameFramework.Interfaces.Support
{
	/// <summary>
	/// Do after server registration.
	/// </summary>
	public interface IAfterServerRegistration
	{
		/// <summary>
		/// Do something after register to the server.
		/// </summary>
		/// <param name="serverPeer">Our server peer which after register.</param>
		void AfterRegister(IServerPeer serverPeer);
	}
}
