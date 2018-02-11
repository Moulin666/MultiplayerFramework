using GameCommon;
using MultiplayerGameFramework.Implementation.Messaging;
using MultiplayerGameFramework.Interfaces.Client;
using MultiplayerGameFramework.Interfaces.Config;
using MultiplayerGameFramework.Interfaces.Messaging;
using MultiplayerGameFramework.Interfaces.Server;
using Servers.Config;

namespace Servers.Handlers.ProxyServerHandlers
{
	public class ChatProxyHandler : IHandler<IClientPeer>
	{
		private readonly IServerConnectionCollection<IServerType, IServerPeer> _serverCollection;

		public ChatProxyHandler (IServerConnectionCollection<IServerType, IServerPeer> serverCollection)
		{
			_serverCollection = serverCollection;
		}

		public MessageType Type => MessageType.Request;

		public byte Code => (byte)MessageOperationCode.ChatOperationCode;

		public int? SubCode => null;

		public bool HandleMessage(IMessage message, IClientPeer peer)
		{
			message.Parameters.Add((byte)MessageParameterCode.PeerIdParameterCode, peer.PeerId.ToByteArray());

			var servers = _serverCollection.GetServersByType<IServerPeer>(ServerType.ChatServer);

			var request = new Request(message.Code, message.SubCode, message.Parameters);

			foreach(var server in servers)
			{
				server.SendMessage(request);
			}

			return true;
		}
	}
}
