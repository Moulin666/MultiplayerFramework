using GameCommon;
using UnityEngine;

public class LoginHandler : IMessageHandler
{
	public MessageType Type => MessageType.Response;

	public byte Code => (byte)MessageOperationCode.LoginOperationCode;

	public int? SubCode => (int)MessageSubCode.LoginSubCode;

	public bool HandleMessage(IMessage message)
	{
		var response = message as Response;

		if (response.ReturnCode == (int)ReturnCode.OK)
		{
			Loading.Load(LoadingScene.Lobby);
			return true;
		}

		Debug.LogFormat("Notify about this to user. Msg = {0}", response.DebugMessage);

		return true;
	}
}
