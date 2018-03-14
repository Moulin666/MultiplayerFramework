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

namespace Servers.Handlers.ChatServerHandlers
{
	public class ClientChatForwardingRequestHandler : IHandler<IClientPeer>
	{
		private ILogger log;
		private IClientCodeRemover codeRemover;
		private IServerConnectionCollection<IServerType, IServerPeer> connectionCollection;

		public ClientChatForwardingRequestHandler(ILogger log, IClientCodeRemover codeRemover,
			IServerConnectionCollection<IServerType, IServerPeer> connectionCollection)
		{
			this.log = log;
			this.codeRemover = codeRemover;
			this.connectionCollection = connectionCollection; 
		}

		public MessageType Type => MessageType.Async | MessageType.Request | MessageType.Response;

		public byte Code => (byte)MessageOperationCode.ChatOperationCode;

		public int? SubCode => null;

		public bool HandleMessage(IMessage message, IClientPeer peer)
		{
			var messageForwarded = false;

			var chatServers = connectionCollection.GetServersByType<IServerPeer>(ServerType.ChatServer);
			log.DebugFormat("Received message to the ChatServer. Found {0} chat servers.", chatServers.Count);

			codeRemover.RemoveCodes(message);
			message.Parameters.Add((byte)MessageParameterCode.PeerIdParameterCode, peer.PeerId.ToByteArray());

			var chatServer = chatServers.FirstOrDefault();
			if (chatServer != null)
			{
				chatServer.SendMessage(message);
				messageForwarded = true;
			}

			return messageForwarded;
		}
    }
}
