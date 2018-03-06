using ExitGames.Client.Photon;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using GameCommon;
using GameCommon.MessageObjects;

public class RegisterView : MonoBehaviour
{
	#region Public variables

	public InputField Login;
	public InputField Password;
	public InputField ConfirmPassword;
	public InputField Email;

	public InputField CharacterName;

	public Slider SexSlider;
	public Slider CharacterTypeSlider;
	public Slider ClassSlider;
	public Slider SubClassSlider;

	public Text Info;

	#endregion

	#region Private variables
	
	private string Sex = "Male";
	private int CharacterType = 1;
	private string Class = "Rogue";
	private string SubClass = "Cleric";

	#endregion

	private void Start()
	{
		RegisterHandler.OnGetInfo += GetInfo;
	}

	private void OnDestroy()
	{
		RegisterHandler.OnGetInfo -= GetInfo;
	}

	public void SliderValueChange(int sliderId)
	{
		switch (sliderId)
		{
			case 1:
				switch ((int)SexSlider.value)
				{
					case 1:
						Sex = "Male";
						break;
					case 2:
						Sex = "Female";
						break;
				}

				SexSlider.transform.Find("Text").GetComponent<Text>().text = Sex;
				break;
			case 2:
				CharacterType = (int)CharacterTypeSlider.value;
				break;
			case 3:
				switch ((int)ClassSlider.value)
				{
					case 1:
						Class = "Warrior";
						break;
					case 2:
						Class = "Rogue";
						break;
					case 3:
						Class = "Mage";
						break;
				}

				ClassSlider.transform.Find("Text").GetComponent<Text>().text = Class;
				break;
			case 4:
				switch ((int)SubClassSlider.value)
				{
					case 1:
						SubClass = "Cleric";
						break;
					case 2:
						SubClass = "Warlock";
						break;
				}

				SubClassSlider.transform.Find("Text").GetComponent<Text>().text = SubClass;
				break;
		}
	}

	public void BackClick()
	{
		Loading.Load(LoadingScene.Login);
	}

	public void SendRegisterRequest()
	{
		if (Login.text.Length < 6 || Password.text.Length < 6)
		{
			Info.text = "Login and password can't be less than 6 symbols";
			return;
		}
		else if (Login.text.Length > 16 || Password.text.Length > 16)
		{
			Info.text = "Login and password can't be more than 16 symbols";
			return;
		}

		if (Password.text != ConfirmPassword.text)
		{
			Info.text = "Passwords don't match";
			return;
		}

		if (CharacterName.text.Length < 6)
		{
			Info.text = "Name of your character can't be less than 6 symbols.";
			return;
		}
		else if (CharacterName.text.Length > 16)
		{
			Info.text = "Name of your character can't be more than 16 symbols";
			return;
		}

		var serializeCharacterData = MessageSerializerService
			.SerializeObjectOfType<RegisterCharacterData>(new RegisterCharacterData
			{
				CharacterName = CharacterName.text,
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
				{ (byte)MessageParameterCode.Login, Login.text },
				{ (byte)MessageParameterCode.Password, Password.text },
				{ (byte)MessageParameterCode.Email, Email.text },
				{ (byte)MessageParameterCode.CharacterRegisterData, serializeCharacterData.ToString() }
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
