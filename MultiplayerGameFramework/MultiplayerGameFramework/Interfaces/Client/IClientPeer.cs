using MultiplayerGameFramework.Interfaces.Messaging;
using System;

namespace MultiplayerGameFramework.Interfaces.Client
{
	/// <summary>
	/// Client Peer.
	/// </summary>
	public interface IClientPeer
	{
		/// <summary>
		/// Is Proxy?
		/// </summary>
		bool IsProxy { get; set; }
		/// <summary>
		/// Peer Id.
		/// </summary>
		Guid PeerId { get; set; }
		/// <summary>
		/// Client data.
		/// </summary>
		/// <typeparam name="T">Class, IClientData.</typeparam>
		/// <returns></returns>
		T ClientData<T>() where T : class, IClientData;

		/// <summary>
		/// Disconnect from the server.
		/// </summary>
		void Disconnect();
		/// <summary>
		/// Send message to the client.
		/// </summary>
		/// <param name="message">Message for sent.</param>
		void SendMessage(IMessage message);
	}
}
