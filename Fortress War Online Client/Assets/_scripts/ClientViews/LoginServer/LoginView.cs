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
		string Login = "Moulin666";
		string Password = "123456";

		if (Login.Length < 6 || Password.Length < 6)
		{
			Debug.Log("Login and password can't be less than 6 symbols");
			return;
		}

		OperationRequest request = new OperationRequest()
		{
			OperationCode = (byte)MessageOperationCode.LoginOperationCode,
			Parameters = new Dictionary<byte, object>
			{
				{ PhotonEngine.Instance.SubCodeParameterCode, (int)MessageSubCode.LoginSubCode },
				{ (byte)MessageParameterCode.Login, Login },
				{ (byte)MessageParameterCode.Password, Password }
			}
		};

		PhotonEngine.Instance.SendRequest(request);
	}
}
