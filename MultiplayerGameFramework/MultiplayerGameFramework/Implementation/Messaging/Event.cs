using MultiplayerGameFramework.Interfaces.Messaging;
using System.Collections.Generic;

namespace MultiplayerGameFramework.Implementation.Messaging
{
	public class Event : IMessage
	{
		private readonly byte _code;
		private readonly Dictionary<byte, object> _parameters;
		private readonly int? _subCode;

		public Event(byte code, int? subCode, Dictionary<byte, object> parameters)
		{
			_code = code;
			_subCode = subCode;
			_parameters = parameters;
		}

		public MessageType Type
		{
			get
			{
				return MessageType.Async;
			}
		}

		public byte Code
		{
			get
			{
				return _code;
			}
		}

		public int? SubCode
		{
			get
			{
				return _subCode;
			}
		}

		public Dictionary<byte, object> Parameters
		{
			get
			{
				return _parameters;
			}
		}
	}
}
