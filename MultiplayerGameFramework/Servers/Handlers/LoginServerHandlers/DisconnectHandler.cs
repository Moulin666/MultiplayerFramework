using System;
using System.Linq;
using ExitGames.Logging;
using GameCommon;
using MultiplayerGameFramework.Implementation.Messaging;
using MultiplayerGameFramework.Interfaces.Client;
using MultiplayerGameFramework.Interfaces.Messaging;
using MultiplayerGameFramework.Interfaces.Server;

namespace Servers.Handlers
{
	public class DisconnectHandler : IHandler<IServerPeer>
	{
		private ILogger log;
		private IConnectionCollection<IClientPeer> connectionCollection;

		public DisconnectHandler(ILogger log, IConnectionCollection<IClientPeer> connectionCollection)
		{
			this.log = log;
			this.connectionCollection = connectionCollection;
		}

		public MessageType Type => MessageType.Request;

		public byte Code => (byte)MessageOperationCode.LoginOperationCode;

		public int? SubCode => 0;

		public bool HandleMessage(IMessage message, IServerPeer peer)
		{
			var clientPeer = connectionCollection.GetPeers<IClientPeer>().FirstOrDefault(p => p.PeerId ==
				new Guid((Byte[])message.Parameters[(byte)MessageParameterCode.PeerIdParameterCode]));

			connectionCollection.Disconnect(clientPeer);

			log.DebugFormat("Client disconnect. Clients - {0}", connectionCollection.GetPeers<IClientPeer>().Count);

			return true;
		}
	}
}
