using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameCommon;
using MultiplayerGameFramework.Implementation.Messaging;
using MultiplayerGameFramework.Interfaces.Messaging;
using MultiplayerGameFramework.Interfaces.Server;

namespace Servers.Handlers.ChatServerHandlers
{
	public class ChatRegisterHandler : IHandler<IServerPeer>
	{
		public ChatRegisterHandler()
		{

		}

		public MessageType Type => MessageType.Request; // or async

		public byte Code => (byte)MessageOperationCode.ChatOperationCode;

		public int? SubCode => 0; // sub code create for this. Register chat request/async

		public bool HandleMessage(IMessage message, IServerPeer peer)
		{
			// register new character in chat server

			return true;
		}
	}
}
