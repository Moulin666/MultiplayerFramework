using GameCommon;
using MultiplayerGameFramework.Implementation.Messaging;
using MultiplayerGameFramework.Interfaces.Messaging;
using MultiplayerGameFramework.Interfaces.Server;
using System.Collections.Generic;

namespace Servers.Handlers
{
	public class TestGameHandler : IHandler<IServerPeer>
	{
		public MessageType Type => MessageType.Request;

		public byte Code => (byte)MessageOperationCode.GameOperationCode;

		public int? SubCode => (int)MessageSubCode.TestGameSubCode;

		public bool HandleMessage(IMessage message, IServerPeer peer)
		{
			var msg = string.Format("Response from game server. Client request - {0}", message.Parameters[(byte)MessageParameterCode.TestMessageParameterCode]);

			Response response = new Response(Code, SubCode, new Dictionary<byte, object>()
			{
				{ (byte)MessageParameterCode.SubCodeParameterCode, SubCode },
				{ (byte)MessageParameterCode.PeerIdParameterCode, message.Parameters[(byte)MessageParameterCode.PeerIdParameterCode] },
				{ (byte)MessageParameterCode.TestMessageParameterCode, msg }
			});

			peer.SendMessage(response);

			return true;
		}
	}
}
