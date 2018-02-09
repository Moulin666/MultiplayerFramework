using GameCommon;
using UnityEngine;

namespace Assets._scripts.ClientHandlers
{
	public class GameTestHandler : IMessageHandler
	{
		public MessageType Type => MessageType.Response;

		public byte Code => (byte)MessageOperationCode.GameOperationCode;

		public int? SubCode => (int)MessageSubCode.TestGameSubCode;

		public bool HandleMessage(IMessage message)
		{
			Debug.Log(message.Parameters[(byte)MessageParameterCode.TestMessageParameterCode]);

			return true;
		}
	}
}
