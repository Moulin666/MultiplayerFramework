using ExitGames.Client.Photon;
using UnityEngine;
using System.Collections.Generic;
using GameCommon;
using UnityEngine.UI;

public class LoginView : MonoBehaviour
{
	public InputField Login;
	public InputField Password;

	public Text Info;

	private void Start()
	{
		LoginHandler.OnGetInfo += GetInfo;
	}

	private void OnDestroy()
	{
		LoginHandler.OnGetInfo -= GetInfo;
	}

	public void ToRegisterClick()
	{
		if (PhotonEngine.Instance.State != EngineState.ConnectedState)
			return;

		Loading.Load(LoadingScene.Register);
	}

	public void SendLoginRequest()
	{
		if (Login.text.Length < 6 || Password.text.Length < 6)
		{
			Info.text = "Login and password can't be less than 6 symbols";
			return;
		}

		OperationRequest request = new OperationRequest()
		{
			OperationCode = (byte)MessageOperationCode.LoginOperationCode,
			Parameters = new Dictionary<byte, object>
			{
				{ PhotonEngine.Instance.SubCodeParameterCode, (int)MessageSubCode.LoginSubCode },
				{ (byte)MessageParameterCode.Login, Login.text },
				{ (byte)MessageParameterCode.Password, Password.text }
			}
		};

		PhotonEngine.Instance.SendRequest(request);
	}

	public void GetInfo(string info)
	{
		if (Info != null)
			Info.text = info;
	}
}
