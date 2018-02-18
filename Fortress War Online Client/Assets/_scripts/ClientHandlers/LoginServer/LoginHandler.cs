using GameCommon;
using UnityEngine;

public class LoginHandler : IMessageHandler
{
	public delegate void InfoDelegate(string info);
	public static event InfoDelegate OnGetInfo;

	public MessageType Type => MessageType.Response;

	public byte Code => (byte)MessageOperationCode.LoginOperationCode;

	public int? SubCode => (int)MessageSubCode.LoginSubCode;

	public bool HandleMessage(IMessage message)
	{
		var response = message as Response;

		switch (response.ReturnCode)
		{
			case (int)ReturnCode.OK:
				{
					Loading.Load(LoadingScene.Lobby);
					OnGetInfo?.Invoke("Login success.");
					return true;
				}
			case (int)ReturnCode.OperationInvalid:
			case (int)ReturnCode.LoginOrPasswordIncorrect:
				{
					OnGetInfo?.Invoke(response.DebugMessage);
					return true;
				}
			default:
				{
					OnGetInfo?.Invoke("OperationDenied.");
					Debug.LogFormat("OperationDenied. DebugMessage = {0}", response.DebugMessage);
					return true;
				}
		}
	}
}
