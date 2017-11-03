using MultiplayerGameFramework.Implementation.Messaging;

namespace MultiplayerGameFramework.Interfaces.Messaging
{
	public interface IHandler<T>
	{
		MessageType Type { get; }
		byte Code { get; }
		int? SubCode { get; }

		bool HandleMessage(IMessage message, T peer);
	}
}
