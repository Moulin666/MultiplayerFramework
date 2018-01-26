using MultiplayerGameFramework.Interfaces.Messaging;
using MultiplayerGameFramework.Interfaces.Server;

namespace MultiplayerGameFramework.Implementation.Messaging
{
	/// <summary>
	/// Server handler.
	/// </summary>
	public abstract class ServerHandler : IHandler<IServerPeer>
	{
		/// <summary>
		/// Message type for handler.
		/// </summary>
		public abstract MessageType Type { get; }
		/// <summary>
		/// GameCommon.OperationCode.
		/// </summary>
		public abstract byte Code { get; }
		/// <summary>
		/// GameCommon.SubCode.
		/// </summary>
		public abstract int? SubCode { get; }

		/// <summary>
		/// Handling operation.
		/// </summary>
		/// <param name="message">Message from client.</param>
		/// <param name="peer">Client.</param>
		/// <returns>bool with handle state.</returns>
		public bool HandleMessage(IMessage message, IServerPeer peer)
		{
			return OnHandleMessage(message, peer);
		}

		/// <summary>
		/// Handle operation.
		/// </summary>
		/// <param name="message">Message from client.</param>
		/// <param name="peer">Client.</param>
		/// <returns>bool with handle state.</returns>
		public abstract bool OnHandleMessage(IMessage message, IServerPeer peer);
	}
}
