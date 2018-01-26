using System.Collections.Generic;

public class Response : IMessage
{
	/// <summary>
	/// GameCommon.MessageOperationCode.
	/// </summary>
	protected readonly byte _code;
	/// <summary>
	/// GameCommon.MessageSubCode.
	/// </summary>
	protected readonly int? _subCode;
	/// <summary>
	/// Dictionary of parameters, byte is GameCommon.MessageParameterCode, object is something.
	/// </summary>
	protected readonly Dictionary<byte, object> _parameters;
	/// <summary>
	/// DebugMessage for logs on the server.
	/// </summary>
	protected readonly string _debugMessage;
	/// <summary>
	/// ReturnCode from the server
	/// </summary>
	protected readonly short _returnCode;

	/// <summary>
	/// Message.MessageType
	/// </summary>
	public MessageType Type
	{
		get
		{
			return MessageType.Response;
		}
	}

	/// <summary>
	/// Return GameCommon.MessageOperationCode.
	/// </summary>
	public byte Code { get { return _code; } }
	/// <summary>
	/// Return GameCommon.MessageSubCode.
	/// </summary>
	public int? SubCode { get { return _subCode; } }
	/// <summary>
	/// Return dictionary of parameters, byte is GameCommon.MessageParameterCode, object is something.
	/// </summary>
	public Dictionary<byte, object> Parameters { get { return _parameters; } }
	/// <summary>
	/// Return DebugMessage for logs on the server.
	/// </summary>
	public string DebugMessage { get { return _debugMessage; } }
	/// <summary>
	/// Return ReturnCode from the server
	/// </summary>
	public short ReturnCode { get { return _returnCode; } }

	/// <summary>
	/// Base constructor Response.
	/// Create Response with GameCommon.MessageOperationCode, GameCommon.MessageSubCode and GameCommon.MessageParameterCode.
	/// </summary>
	/// <param name="code">GameCommon.MessageOperationCode.</param>
	/// <param name="subCode">GameCommon.MessageSubCode.</param>
	/// <param name="parameters">GameCommon.MessageParameterCode.</param>
	public Response (byte code, int? subCode, Dictionary<byte, object> parameters)
	{
		_code = code;
		_subCode = subCode;
		_parameters = parameters;
	}

	/// <summary>
	/// Extended constructor Response.
	/// Create Response with GameCommon.MessageOperationCode, GameCommon.MessageSubCode, GameCommon.MessageParameterCode,
	/// DebugMessage and ReturnCode.
	/// </summary>
	/// <param name="code">GameCommon.MessageOperationCode.</param>
	/// <param name="subCode">GameCommon.MessageSubCode.</param>
	/// <param name="parameters">GameCommon.MessageParameterCode.</param>
	/// <param name="debugMessage">DebugMessage for logs on the server.</param>
	/// <param name="returnCode">ReturnCode from the server.</param>
	public Response(byte code, int? subCode, Dictionary<byte, object> parameters, string debugMessage, short returnCode)
		: this(code, subCode, parameters)
	{
		_debugMessage = debugMessage;
		_returnCode = returnCode;
	}
}
