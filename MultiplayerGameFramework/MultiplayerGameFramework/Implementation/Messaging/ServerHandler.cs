using MultiplayerGameFramework.Interfaces.Messaging;
using MultiplayerGameFramework.Interfaces.Server;

namespace MultiplayerGameFramework.Implementation.Messaging
{
	public abstract class ServerHandler : IHandler<IServerPeer>
	{
		public abstract MessageType Type { get; }
		public abstract byte Code { get; }
		public abstract int? SubCode { get; }

		public bool HandleMessage(IMessage message, IServerPeer peer)
		{
			return OnHandleMessage(message, peer);
		}

		public abstract bool OnHandleMessage(IMessage message, IServerPeer peer);
	}
}
