using ExitGames.Logging;
using GameCommon;
using MGF_Photon.Implementation.Server;
using MultiplayerGameFramework.Implementation.Messaging;
using MultiplayerGameFramework.Interfaces.Messaging;
using MultiplayerGameFramework.Interfaces.Server;

namespace Servers.Handlers
{
	public class WorldEnterHandler : IHandler<IServerPeer>
	{
		private ILogger log;

		public WorldEnterHandler(ILogger log)
		{
			this.log = log;
		}

		public MessageType Type => MessageType.Request;

		public byte Code => (byte)MessageOperationCode.GameOperationCode;

		public int? SubCode => (int)MessageSubCode.WorldEnterSubCode;

		public bool HandleMessage(IMessage message, IServerPeer peer)
		{
			var serverPeer = peer as PhotonServerPeer;

			// create user

			// world add user

			// add user in BEPU Physics. PhysicsBackgroundThread

			// notify all about enter this user in world

			// send response with user data

			return true;
		}
	}
}
