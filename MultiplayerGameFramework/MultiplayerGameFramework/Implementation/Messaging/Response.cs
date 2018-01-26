using MultiplayerGameFramework.Interfaces.Messaging;
using System.Collections.Generic;

namespace MultiplayerGameFramework.Implementation.Messaging
{
	/// <summary>
	/// Response for handling.
	/// </summary>
	public class Response : IMessage
	{
		private readonly byte _code;
		private readonly Dictionary<byte, object> _parameters;
		private readonly int? _subCode;

		private readonly string _debugMessage;
		private readonly short _returnCode;

		/// <summary>
		/// Base constructor.
		/// </summary>
		/// <param name="code">GameCommon.OperationCode.</param>
		/// <param name="subCode">GameCommon.SubCode.</param>
		/// <param name="parameters">GameCommon.ParameterCode.</param>
		public Response(byte code, int? subCode, Dictionary<byte, object> parameters)
		{
			_code = code;
			_subCode = subCode;
			_parameters = parameters;
		}

		/// <summary>
		/// Extended constructor.
		/// </summary>
		/// <param name="code">GameCommon.OperationCode.</param>
		/// <param name="subCode">GameCommon.SubCode.</param>
		/// <param name="parameters">GameCommon.ParameterCode.</param>
		/// <param name="debugMessage">Debug message.</param>
		/// <param name="returnCode">Return code.</param>
		public Response(byte code, int? subCode, Dictionary<byte, object> parameters, string debugMessage, short returnCode)
			: this(code, subCode, parameters)
		{
			_debugMessage = debugMessage;
			_returnCode = returnCode;
		}
		
		/// <summary>
		/// Type of message.
		/// </summary>
		public MessageType Type
		{
			get
			{
				return MessageType.Response;
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

		/// <summary>
		/// Debug message.
		/// </summary>
		public string DebugMessage
		{
			get
			{
				return _debugMessage;
			}
		}
		/// <summary>
		/// Return code.
		/// </summary>
		public short ReturnCode
		{
			get
			{
				return _returnCode;
			}
		}
	}
}
