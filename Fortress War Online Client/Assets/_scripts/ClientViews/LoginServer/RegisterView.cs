using ExitGames.Client.Photon;
using UnityEngine;
using System.Collections.Generic;
using GameCommon;
using GameCommon.MessageObjects;

public class RegisterView : MonoBehaviour
{
	// add fields

	public void BackClick()
	{
		Loading.Load(LoadingScene.Login);
	}

	public void SendRegisterRequest()
	{
		string Login = "Moulin666";
		string Password = "123456";
		string Email = "thirtybeltmusicgroup@gmail.com";
		string CharacterName = "Repository";
		string Sex = "Female";
		int CharacterType = 1;
		string Class = "Rogue";
		string SubClass = "Warlock";

		if (Login.Length < 6 || Password.Length < 6)
		{
			Debug.Log("Login and password can't be less than 6 symbols");
			return;
		}

		var serializeCharacterData = MessageSerializerService
			.SerializeObjectOfType<RegisterCharacterData>(new RegisterCharacterData
			{
				CharacterName = CharacterName,
				Sex = Sex,
				CharacterType = CharacterType,
				Class = Class,
				SubClass = SubClass
			});

		OperationRequest request = new OperationRequest()
		{
			OperationCode = (byte)MessageOperationCode.LoginOperationCode,
			Parameters = new Dictionary<byte, object>
			{
				{ PhotonEngine.Instance.SubCodeParameterCode, (int)MessageSubCode.RegisterSubCode },
				{ (byte)MessageParameterCode.Login, Login },
				{ (byte)MessageParameterCode.Password, Password },
				{ (byte)MessageParameterCode.Email, Email },
				{ (byte)MessageParameterCode.CharacterRegisterData, serializeCharacterData.ToString() }
			}
		};

		PhotonEngine.Instance.SendRequest(request);
	}
}
