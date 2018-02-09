using GameCommon;
using UnityEngine;

namespace Assets._scripts.ClientHandlers
{
	public class ChatTestHandler : IMessageHandler
	{
		public MessageType Type => MessageType.Response;

		public byte Code => (byte)MessageOperationCode.ChatOperationCode;

		public int? SubCode => (int)MessageSubCode.TestChatSubCode;

		public bool HandleMessage(IMessage message)
		{
			Debug.Log(message.Parameters[(byte)MessageParameterCode.TestMessageParameterCode]);

			return true;
		}
	}
}
