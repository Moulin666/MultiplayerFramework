using GameCommon;
using MultiplayerGameFramework.Implementation.Messaging;
using MultiplayerGameFramework.Interfaces.Client;
using MultiplayerGameFramework.Interfaces.Config;
using MultiplayerGameFramework.Interfaces.Messaging;
using MultiplayerGameFramework.Interfaces.Server;
using Servers.Config;

namespace Servers.Handlers
{
	public class GameProxyHandler : IHandler<IClientPeer>
	{
		private readonly IServerConnectionCollection<IServerType, IServerPeer> _serverCollection;

		public GameProxyHandler (IServerConnectionCollection<IServerType, IServerPeer> serverCollection)
		{
			_serverCollection = serverCollection;
		}

		public MessageType Type => MessageType.Request;

		public byte Code => (byte)MessageOperationCode.GameOperationCode;

		public int? SubCode => null;

		public bool HandleMessage(IMessage message, IClientPeer peer)
		{
			message.Parameters.Add((byte)MessageParameterCode.PeerIdParameterCode, peer.PeerId.ToByteArray());

			var servers = _serverCollection.GetServersByType<IServerPeer>(ServerType.GameServer);

			var request = new Request(message.Code, message.SubCode, message.Parameters);

			foreach(var server in servers)
			{
				server.SendMessage(request);
			}

			return true;
		}
	}
}
