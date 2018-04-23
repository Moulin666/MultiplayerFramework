using ExitGames.Logging;
using GameCommon;
using MultiplayerGameFramework.Implementation.Messaging;
using MultiplayerGameFramework.Interfaces.Client;
using MultiplayerGameFramework.Interfaces.Messaging;
using MultiplayerGameFramework.Interfaces.Server;
using MultiplayerGameFramework.Interfaces.Support;

namespace Servers.Handlers
{
	public class ChatRegisterHandler : IHandler<IServerPeer>
	{
		private ILogger log;
		private IClientCodeRemover codeRemover;
		private IConnectionCollection<IClientPeer> connectionCollection;

		public ChatRegisterHandler(ILogger log, IClientCodeRemover codeRemover,
			IConnectionCollection<IClientPeer> connectionCollection)
		{
			this.log = log;
			this.codeRemover = codeRemover;
			this.connectionCollection = connectionCollection;
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
