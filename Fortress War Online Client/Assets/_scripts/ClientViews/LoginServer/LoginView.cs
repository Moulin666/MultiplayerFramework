using ExitGames.Client.Photon;
using UnityEngine;
using System.Collections.Generic;
using GameCommon;
using UnityEngine.SceneManagement;

public class LoginView : MonoBehaviour
{
	// add fields

	public void ToRegisterClick()
	{
		//SceneManager.LoadScene();
	}

	public void SendLoginRequest()
	{
		// Check field is null or empty

		OperationRequest request = new OperationRequest()
		{
			OperationCode = (byte)MessageOperationCode.LoginOperationCode,
			Parameters = new Dictionary<byte, object>
			{
				{ PhotonEngine.Instance.SubCodeParameterCode, (int)MessageSubCode.LoginSubCode },
				//{ (byte)MessageParameterCode.LoginParameterCode, Text },
				//{ (byte)MessageParameterCode.PasswordParameterCode, Text }
			}
		};

		Debug.LogFormat("Delete that. Send request with code - ", request.OperationCode);
		//PhotonEngine.Instance.SendRequest(request);
	}
}
