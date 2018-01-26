using System.Collections.Generic;

public interface IMessage
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
	/// GameCommon.MessageParameterCode.
	/// </summary>
	Dictionary<byte, object> Parameters { get; }
}
