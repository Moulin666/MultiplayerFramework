public interface IMessageHandler
{
	/// <summary>
	/// Type of message.
	/// </summary>
	MessageType Type { get; }

	/// <summary>
	/// GameCommon.MessageOperationCode.
	/// </summary>
	byte Code { get; }

	/// <summary>
	/// GameCommon.MessageSubCode.
	/// </summary>
	int? SubCode { get; }

	/// <summary>
	/// Implement handle.
	/// </summary>
	bool HandleMessage(IMessage message);
}
