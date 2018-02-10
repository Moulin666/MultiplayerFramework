using ExitGames.Client.Photon;
using UnityEngine;
using System.Collections.Generic;
using GameCommon;

public class RegisterView : MonoBehaviour
{
	// add fields

	public void BackClick()
	{
		Loading.Load(LoadingScene.Login);
	}

	public void SendRegisterRequest()
	{
		// Check field is null or empty and check password and confirm password

		OperationRequest request = new OperationRequest()
		{
			OperationCode = (byte)MessageOperationCode.LoginOperationCode,
			Parameters = new Dictionary<byte, object>
			{
				{ PhotonEngine.Instance.SubCodeParameterCode, (int)MessageSubCode.RegisterSubCode },
				// login, password, email
				// (byte)MessageParameterCode.CharacterCreate - 
				// CharacterName, Sex, Class, SubClass, CharacterType, CharacterHeight
			}
		};

		Debug.LogFormat("Delete that. Send request with code - ", request.OperationCode);
		//PhotonEngine.Instance.SendRequest(request);
	}
}
