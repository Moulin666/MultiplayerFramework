using ExitGames.Client.Photon;
using UnityEngine;
using System.Collections.Generic;

public class TestView : MonoBehaviour
{
	public void SendResponseRequest()
	{
		OperationRequest request = new OperationRequest()
		{
			OperationCode = 1,
			Parameters = new Dictionary<byte, object>
			{
				{ PhotonEngine.instance.SubCodeParameterCode, 1 }
			}
		};

		PhotonEngine.instance.SendRequest(request);

		Debug.Log("Send request for get response from server");
	}

	public void SendEventRequest()
	{
		OperationRequest request = new OperationRequest()
		{
			OperationCode = 1,
			Parameters = new Dictionary<byte, object>
			{
				{ (byte)PhotonEngine.instance.SubCodeParameterCode, 2 }
			}
		};

		PhotonEngine.instance.SendRequest(request);

		Debug.Log("Send request for get event from server");
	}
}
