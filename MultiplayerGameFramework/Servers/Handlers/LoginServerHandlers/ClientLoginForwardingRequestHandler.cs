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

namespace Servers.Handlers.LoginServerHandlers
{
	public class ClientLoginForwardingRequestHandler : IHandler<IClientPeer>
	{
		private ILogger log;
		private IClientCodeRemover codeRemover;
		private IServerConnectionCollection<IServerType, IServerPeer> connectionCollection;

		public ClientLoginForwardingRequestHandler(ILogger log, IClientCodeRemover codeRemover,
			IServerConnectionCollection<IServerType, IServerPeer> connectionCollection)
		{
			this.log = log;
			this.codeRemover = codeRemover;
			this.connectionCollection = connectionCollection;
		}

		public MessageType Type => MessageType.Async | MessageType.Request | MessageType.Response;

		public byte Code => (byte)MessageOperationCode.LoginOperationCode;

		public int? SubCode => null;

		public bool HandleMessage(IMessage message, IClientPeer peer)
		{
			var messageForwarded = false;

			var loginServers = connectionCollection.GetServersByType<IServerPeer>(ServerType.LoginServer);
			log.DebugFormat("Received message to the LoginServer. Found {0} login servers.", loginServers.Count);

			codeRemover.RemoveCodes(message);
			message.Parameters.Add((byte)MessageParameterCode.PeerIdParameterCode, peer.PeerId.ToByteArray());

			var loginServer = loginServers.FirstOrDefault();
			if (loginServer != null)
			{
				loginServer.SendMessage(message);
				messageForwarded = true;
			}

			return messageForwarded;
		}
	}
}
