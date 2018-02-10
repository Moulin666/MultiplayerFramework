using System.Collections.Generic;
using GameCommon;
using MGF_Photon.Implementation.Server;
using MultiplayerGameFramework.Implementation.Messaging;
using MultiplayerGameFramework.Interfaces.Messaging;
using MultiplayerGameFramework.Interfaces.Server;
using Servers.Handlers.LoginServerHandlers.Operations;

namespace Servers.Handlers
{
	public class LoginHandler : IHandler<IServerPeer>
	{
		public MessageType Type => MessageType.Request;

		public byte Code => (byte)MessageOperationCode.LoginOperationCode;

		public int? SubCode => (int)MessageSubCode.RegisterSubCode;

		public bool HandleMessage(IMessage message, IServerPeer peer)
		{
			var serverPeer = peer as PhotonServerPeer;
			var operation = new LoginOperation(serverPeer.protocol, message);

			if (!operation.IsValid)
			{
				Response response = new Response(Code, SubCode, new Dictionary<byte, object>()
				{
					{ (byte)MessageParameterCode.SubCodeParameterCode, SubCode },
					{ (byte)MessageParameterCode.PeerIdParameterCode, message.Parameters[(byte)MessageParameterCode.PeerIdParameterCode] },
				}, operation.GetErrorMessage(), (int)ReturnCode.OperationInvalid);

				peer.SendMessage(response);

				return true;
			}

			// TO DO : LOGIN HANDLING

			return true;
		}
	}
}
