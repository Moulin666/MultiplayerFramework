using MultiplayerGameFramework.Interfaces.Client;
using MultiplayerGameFramework.Interfaces.Messaging;

namespace MultiplayerGameFramework.Implementation.Messaging
{
	public abstract class ClientHandler : IHandler<IClientPeer>
	{
		public abstract MessageType Type { get; }
		public abstract byte Code { get; }
		public abstract int? SubCode { get; }

		public bool HandleMessage (IMessage message, IClientPeer peer)
		{
			return OnHandleMessage(message, peer);
		}

		protected abstract bool OnHandleMessage(IMessage message, IClientPeer peer);
	}
}
