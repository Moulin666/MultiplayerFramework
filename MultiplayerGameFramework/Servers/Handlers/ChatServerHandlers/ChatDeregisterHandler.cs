﻿using GameCommon;
using MultiplayerGameFramework.Implementation.Messaging;
using MultiplayerGameFramework.Interfaces.Messaging;
using MultiplayerGameFramework.Interfaces.Server;

namespace Servers.Handlers
{
	public class ChatDeregisterHandler : IHandler<IServerPeer>
	{
		public ChatDeregisterHandler()
		{

		}

		public MessageType Type => MessageType.Request; // or async

		public byte Code => MessageOperationCode.ChatOperationCode;

		public int? SubCode => 0; // sub code create for this. Deregister chat request/async

		public bool HandleMessage(IMessage message, IServerPeer peer)
		{
			// Deregister new character in chat server

			return true;
		}
	}
}
