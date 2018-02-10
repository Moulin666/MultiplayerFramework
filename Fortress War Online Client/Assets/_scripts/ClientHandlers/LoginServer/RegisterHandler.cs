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

		if (response.ReturnCode == (int)ReturnCode.OK)
		{
			Loading.Load(LoadingScene.Login);
			return true;
		}

		Debug.LogFormat("Notify about this to user. Msg = {0}", response.DebugMessage);

		return true;
	}
}
