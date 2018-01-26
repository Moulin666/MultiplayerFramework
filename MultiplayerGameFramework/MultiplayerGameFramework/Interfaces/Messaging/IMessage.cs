using MultiplayerGameFramework.Implementation.Messaging;
using System.Collections.Generic;

namespace MultiplayerGameFramework.Interfaces.Messaging
{
	/// <summary>
	/// Message for handling.
	/// </summary>
	public interface IMessage
	{
		/// <summary>
		/// Type of message.
		/// </summary>
		MessageType Type { get; }
		/// <summary>
		/// GameCommon.OperationCode.
		/// </summary>
		byte Code { get; }
		/// <summary>
		/// GameCommon.SubCode.
		/// </summary>
		int? SubCode { get; }
		/// <summary>
		/// Dictionary of parameters. Where key, byte - GameCommon.ParameterCode, object - parameter.
		/// </summary>
		Dictionary<byte, object> Parameters { get; }
	}
}
