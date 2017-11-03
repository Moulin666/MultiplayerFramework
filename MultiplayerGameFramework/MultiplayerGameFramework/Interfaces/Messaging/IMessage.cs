using MultiplayerGameFramework.Implementation.Messaging;
using System.Collections.Generic;

namespace MultiplayerGameFramework.Interfaces.Messaging
{
	public interface IMessage
	{
		MessageType Type { get; }
		byte Code { get; }
		int? SubCode { get; }
		Dictionary<byte, object> Parameters { get; }
	}
}
