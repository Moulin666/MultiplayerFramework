using MultiplayerGameFramework.Implementation.Messaging;
using MultiplayerGameFramework.Interfaces.Client;
using MultiplayerGameFramework.Interfaces.Messaging;
using System.Collections.Generic;

namespace Servers.Handlers
{
	public class TestRequestEventHandler : IHandler<IClientPeer>
	{
		public MessageType Type { get { return MessageType.Request; } }

		public byte Code { get { return 1; } }

		public int? SubCode { get { return 2; } }

		public bool HandleMessage(IMessage message, IClientPeer peer)
		{
			Event _event = new Event(Code, SubCode, new Dictionary<byte, object>());
			peer.SendMessage(_event);

			return true;
		}
	}
}
