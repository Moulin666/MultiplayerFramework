using MultiplayerGameFramework.Implementation.Config;
using MultiplayerGameFramework.Interfaces.Server;

namespace MultiplayerGameFramework
{
	/// <summary>
	/// Server Application.
	/// </summary>
	public interface IServerApplication
	{
		/// <summary>
		/// Sub code for the server. GameCommon.SubCode.
		/// </summary>
		byte SubCodeParameterCode { get; }
		/// <summary>
		/// Path to the server.
		/// </summary>
		string BinaryPath { get; }
		/// <summary>
		/// Application Name from configuration.
		/// </summary>
		string ApplicationName { get; }

		/// <summary>
		/// Setup server.
		/// </summary>
		void Setup();
		/// <summary>
		/// TearDown server.
		/// </summary>
		void TearDown();
		/// <summary>
		/// Failed connection.
		/// </summary>
		/// <param name="errorCode">Code with error.</param>
		/// <param name="errorMessage">Error message.</param>
		/// <param name="state">State object.</param>
		void OnServerConnectionFailed(int errorCode, string errorMessage, object state);
		/// <summary>
		/// Do somthing after server registration
		/// </summary>
		/// <param name="serverPeer">Server Peer.</param>
		void AfterServerRegistration(IServerPeer serverPeer);
		/// <summary>
		/// Do connect to the peer.
		/// </summary>
		/// <param name="peerInfo">MGF.Implementation.PeerInfo.</param>
		void ConnectToPeer(PeerInfo peerInfo);
	}
}
