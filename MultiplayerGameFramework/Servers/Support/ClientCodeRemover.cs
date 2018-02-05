using GameCommon;
using MultiplayerGameFramework.Interfaces.Messaging;
using MultiplayerGameFramework.Interfaces.Support;

namespace Servers.Support
{
	public class ClientCodeRemover : IClientCodeRemover
	{
		public void RemoveCodes(IMessage message)
		{
			message.Parameters.Remove((byte)MessageParameterCode.PeerIdParameterCode);
		}
	}
}
