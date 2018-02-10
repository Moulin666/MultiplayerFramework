using GameCommon;
using UnityEngine;

namespace Assets._scripts.ClientHandlers
{
	public class LoginHandler : IMessageHandler
	{
		public MessageType Type => MessageType.Response;

		public byte Code => (byte)MessageOperationCode.LoginOperationCode;

		public int? SubCode => (int)MessageSubCode.LoginSubCode;

		public bool HandleMessage(IMessage message)
		{
			// Notify user about login. If all okay just go to the lobby scene.
			var response = message as Response;
			Debug.Log(response.DebugMessage);

			return true;
		}
	}
}
