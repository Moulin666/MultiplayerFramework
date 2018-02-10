using ExitGames.Client.Photon;
using UnityEngine;
using System.Collections.Generic;
using GameCommon;

public class LoginView : MonoBehaviour
{
	// add fields

	public void ToRegisterClick()
	{
		Loading.Load(LoadingScene.Register);
	}

	public void SendLoginRequest()
	{
		// Check field is null or empty

		string login = "";
		string password = "";

		OperationRequest request = new OperationRequest()
		{
			OperationCode = (byte)MessageOperationCode.LoginOperationCode,
			Parameters = new Dictionary<byte, object>
			{
				{ PhotonEngine.Instance.SubCodeParameterCode, (int)MessageSubCode.LoginSubCode },
				{ (byte)MessageParameterCode.Login, login },
				{ (byte)MessageParameterCode.Password, password }
			}
		};

		Debug.LogFormat("Delete that. Send request with code - ", request.OperationCode);
		//PhotonEngine.Instance.SendRequest(request);
	}
}
