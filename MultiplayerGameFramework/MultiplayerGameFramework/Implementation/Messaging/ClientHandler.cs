using MultiplayerGameFramework.Interfaces.Client;
using MultiplayerGameFramework.Interfaces.Messaging;

namespace MultiplayerGameFramework.Implementation.Messaging
{
	/// <summary>
	/// Abstract class. Client handler.
	/// </summary>
	public abstract class ClientHandler : IHandler<IClientPeer>
	{
		/// <summary>
		/// Type of message.
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
		/// Handle message.
		/// </summary>
		/// <param name="message"></param>
		/// <param name="peer"></param>
		/// <returns></returns>
		public bool HandleMessage (IMessage message, IClientPeer peer)
		{
			return OnHandleMessage(message, peer);
		}

		/// <summary>
		/// Message handler.
		/// </summary>
		/// <param name="message"></param>
		/// <param name="peer"></param>
		/// <returns></returns>
		protected abstract bool OnHandleMessage(IMessage message, IClientPeer peer);
	}
}
