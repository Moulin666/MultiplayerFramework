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
		// Check field is null or empty and check password and confirm password
		
		var serializeCharacterData = MessageSerializerService
			.SerializeObjectOfType<RegisterData>(new RegisterData
			{
				Login = "Moulin666",
				Password = "123456",
				Email = "thirtybeltmusicgroup@gmail.com",
				CharacterName = "Repository",
				Sex = "Female",
				CharacterType = 1,
				CharacterHeight = 140,
				Class = "Rogue",
				SubClass = "Warlock"
			});

		OperationRequest request = new OperationRequest()
		{
			OperationCode = (byte)MessageOperationCode.LoginOperationCode,
			Parameters = new Dictionary<byte, object>
			{
				{ PhotonEngine.Instance.SubCodeParameterCode, (int)MessageSubCode.RegisterSubCode },
				{ (byte)MessageParameterCode.CharacterRegisterData, serializeCharacterData.ToString() }
			}
		};

		PhotonEngine.Instance.SendRequest(request);
	}
}
