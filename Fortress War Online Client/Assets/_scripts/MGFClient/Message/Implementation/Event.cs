using System.Collections.Generic;

public class Event : IMessage
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
	/// Message.MessageType
	/// </summary>
	public MessageType Type
	{
		get
		{
			return MessageType.Async;
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
	/// Constructor Event.
	/// Create Event with GameCommon.MessageParameterCode, GameCommon.MessageSubCode and GameCommon.MessageParameterCode.
	/// </summary>
	/// <param name="code">GameCommon.MessageOperationCode</param>
	/// <param name="subCode">GameCommon.MessageSubCode</param>
	/// <param name="parameters">GameCommon.MessageParameterCode</param>
	public Event (byte code, int? subCode, Dictionary<byte, object> parameters)
	{
		_code = code;
		_subCode = subCode;
		_parameters = parameters;
	}
}
