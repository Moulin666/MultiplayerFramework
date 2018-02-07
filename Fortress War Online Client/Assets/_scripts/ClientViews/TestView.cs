using ExitGames.Client.Photon;
using UnityEngine;
using System.Collections.Generic;
using GameCommon;

public class TestView : MonoBehaviour
{
	public void SendTestLoginRequest()
	{
		OperationRequest request = new OperationRequest()
		{
			OperationCode = (byte)MessageOperationCode.LoginOperationCode,
			Parameters = new Dictionary<byte, object>
			{
				{ PhotonEngine.Instance.SubCodeParameterCode, (int)MessageSubCode.RegisterSubCode },
				{ (byte)MessageParameterCode.TestMessageParameterCode, "Hello server" }
			}
		};

		PhotonEngine.Instance.SendRequest(request);
	}

	public void SendTestChatRequest()
	{
		OperationRequest request = new OperationRequest()
		{
			OperationCode = (byte)MessageOperationCode.ChatOperationCode,
			Parameters = new Dictionary<byte, object>
			{
				{ PhotonEngine.Instance.SubCodeParameterCode, (int)MessageSubCode.TestChatSubCode },
				{ (byte)MessageParameterCode.TestMessageParameterCode, "Test to chat" }
			}
		};

		PhotonEngine.Instance.SendRequest(request);
	}

	public void SendTestGameRequest()
	{
		OperationRequest request = new OperationRequest()
		{
			OperationCode = (byte)MessageOperationCode.GameOperationCode,
			Parameters = new Dictionary<byte, object>
			{
				{ PhotonEngine.Instance.SubCodeParameterCode, (int)MessageSubCode.TestGameSubCode },
				{ (byte)MessageParameterCode.TestMessageParameterCode, "Test game" }
			}
		};

		PhotonEngine.Instance.SendRequest(request);
	}
}
