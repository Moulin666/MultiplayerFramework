using MultiplayerGameFramework.Interfaces.Config;
using MultiplayerGameFramework.Interfaces.Messaging;

namespace MultiplayerGameFramework.Interfaces.Server
{
	/// <summary>
	/// Server peer, client.
	/// </summary>
	public interface IServerPeer
	{
		/// <summary>
		/// Type of server.
		/// </summary>
		IServerType ServerType { get; set; }
		/// <summary>
		/// Server date.
		/// </summary>
		/// <typeparam name="T">class, IServerData.</typeparam>
		/// <returns>T class, IServer data.</returns>
		T ServerData<T>() where T : class, IServerData;
		/// <summary>
		/// Server now registered?
		/// </summary>
		bool Registered { get; set; }
		/// <summary>
		/// Server application.
		/// </summary>
		IServerApplication Server { get; set; }

		/// <summary>
		/// Disconnect from the server.
		/// </summary>
		void Disconnect();
		/// <summary>
		/// Send message to the server.
		/// </summary>
		/// <param name="message"></param>
		void SendMessage(IMessage message);
		/// <summary>
		/// Handle message from the server.
		/// </summary>
		/// <param name="message"></param>
		void HandleMessage(IMessage message);
	}
}
