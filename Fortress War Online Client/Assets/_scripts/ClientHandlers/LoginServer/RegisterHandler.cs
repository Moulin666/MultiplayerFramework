using GameCommon;
using UnityEngine;

namespace Assets._scripts.ClientHandlers
{
	public class RegisterHandler : IMessageHandler
	{
		public MessageType Type => MessageType.Response;

		public byte Code => (byte)MessageOperationCode.LoginOperationCode;

		public int? SubCode => (int)MessageSubCode.RegisterSubCode;

		public bool HandleMessage(IMessage message)
		{
			// Notify user about register. If all okay just exit to login scene.
			var response = message as Response;
			Debug.Log(response.DebugMessage);

			return true;
		}
	}
}
