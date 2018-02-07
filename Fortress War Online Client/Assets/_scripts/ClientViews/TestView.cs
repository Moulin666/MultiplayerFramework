using ExitGames.Client.Photon;
using UnityEngine;
using System.Collections.Generic;
using GameCommon;

public class TestView : MonoBehaviour
{
	public void SendResponseRequest()
	{
		OperationRequest request = new OperationRequest()
		{
			OperationCode = 1,
			Parameters = new Dictionary<byte, object>
			{
				{ PhotonEngine.Instance.SubCodeParameterCode, 1 }
			}
		};

		PhotonEngine.Instance.SendRequest(request);

		Debug.Log("Send request for get response from server");
	}

    public void SendEventRequest()
	{
		OperationRequest request = new OperationRequest()
		{
			OperationCode = 1,
			Parameters = new Dictionary<byte, object>
			{
				{ PhotonEngine.Instance.SubCodeParameterCode, 2 }
			}
		};

		PhotonEngine.Instance.SendRequest(request);

		Debug.Log("Send request for get event from server");
	}
	
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
}
