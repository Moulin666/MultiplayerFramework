using GameCommon;
using MultiplayerGameFramework.Interfaces.Messaging;
using Photon.SocketServer;
using Photon.SocketServer.Rpc;

namespace Servers.Handlers.LoginServerHandlers.Operations
{
	public class LoginOperation : Operation
	{
		public LoginOperation(IRpcProtocol protocol, IMessage message)
			: base (protocol, new OperationRequest(message.Code, message.Parameters)) {}

		[DataMember(Code = (byte)MessageParameterCode.Login, IsOptional = false)]
		public string Login { get; set; }

		[DataMember(Code = (byte)MessageParameterCode.Password, IsOptional = false)]
		public string Password { get; set; }
	}
}
