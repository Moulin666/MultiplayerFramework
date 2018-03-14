using System.Linq;
using ExitGames.Logging;
using GameCommon;
using MultiplayerGameFramework.Implementation.Messaging;
using MultiplayerGameFramework.Interfaces.Client;
using MultiplayerGameFramework.Interfaces.Config;
using MultiplayerGameFramework.Interfaces.Messaging;
using MultiplayerGameFramework.Interfaces.Server;
using MultiplayerGameFramework.Interfaces.Support;
using Servers.Config;

namespace Servers.Handlers.GameServerHandlers
{
	public class ClientGameForwardingRequestHandler : IHandler<IClientPeer>
	{
		private ILogger log;
		private IClientCodeRemover codeRemover;
		private IServerConnectionCollection<IServerType, IServerPeer> connectionCollection;

		public ClientGameForwardingRequestHandler(ILogger log, IClientCodeRemover codeRemover,
			IServerConnectionCollection<IServerType, IServerPeer> connectionCollection)
		{
			this.log = log;
			this.codeRemover = codeRemover;
			this.connectionCollection = connectionCollection;
		}

		public MessageType Type => MessageType.Async | MessageType.Request | MessageType.Response;

		public byte Code => (byte)MessageOperationCode.GameOperationCode;

		public int? SubCode => null;

		public bool HandleMessage(IMessage message, IClientPeer peer)
		{
			var messageForwarded = false;

			var gameServers = connectionCollection.GetServersByType<IServerPeer>(ServerType.GameServer);
			log.DebugFormat("Received message to the GameServer. Found {0} game servers.", gameServers.Count);

			codeRemover.RemoveCodes(message);
			message.Parameters.Add((byte)MessageParameterCode.PeerIdParameterCode, peer.PeerId.ToByteArray());

			var gameServer = gameServers.FirstOrDefault();
			if (gameServer != null)
			{
				gameServer.SendMessage(message);
				messageForwarded = true;
			}

			return messageForwarded;
		}
	}
}
