using GameCommon;
using UnityEngine;

public class RegisterHandler : IMessageHandler
{
	public MessageType Type => MessageType.Response;

	public byte Code => (byte)MessageOperationCode.LoginOperationCode;

	public int? SubCode => (int)MessageSubCode.RegisterSubCode;

	public bool HandleMessage(IMessage message)
	{
		var response = message as Response;

		switch(response.ReturnCode)
		{
			case (int)ReturnCode.OK:
				{
					Loading.Load(LoadingScene.Login);
					Debug.Log("Notify about this to user. Msg = Register success.");
					return true;
				}
			case (int)ReturnCode.OperationInvalid:
			case (int)ReturnCode.AlreadyExist:
				{
					Debug.LogFormat("Notify about this to user. Msg = {0}", response.DebugMessage);
					return true;
				}
			default:
				{
					Debug.Log("Notify about this to user. Msg = OperationDenied.");
					Debug.LogFormat("OperationDenied. DebugMessage = {0}", response.DebugMessage);
					return true;
				}
		}
	}
}
