using MultiplayerGameFramework.Interfaces.Messaging;
using System.Collections.Generic;

namespace MultiplayerGameFramework.Implementation.Messaging
{
	/// <summary>
	/// Event for handling.
	/// </summary>
	public class Event : IMessage
	{
		private readonly byte _code;
		private readonly Dictionary<byte, object> _parameters;
		private readonly int? _subCode;

		/// <summary>
		/// Base constructor.
		/// </summary>
		/// <param name="code">GameCommon.OperationCode.</param>
		/// <param name="subCode">GameCommon.SubCode.</param>
		/// <param name="parameters"><GameCommon.ParameterCode.</param>
		public Event(byte code, int? subCode, Dictionary<byte, object> parameters)
		{
			_code = code;
			_subCode = subCode;
			_parameters = parameters;
		}

		/// <summary>
		/// Type of message.
		/// </summary>
		public MessageType Type
		{
			get
			{
				return MessageType.Async;
			}
		}
		/// <summary>
		/// GameCommon.OperationCode.
		/// </summary>
		public byte Code
		{
			get
			{
				return _code;
			}
		}
		/// <summary>
		/// GameCommon.SubCode.
		/// </summary>
		public int? SubCode
		{
			get
			{
				return _subCode;
			}
		}
		/// <summary>
		/// Dictionary of parameters. Where key, byte - GameCommon.ParameterCode, object - parameter.
		/// </summary>
		public Dictionary<byte, object> Parameters
		{
			get
			{
				return _parameters;
			}
		}
	}
}
