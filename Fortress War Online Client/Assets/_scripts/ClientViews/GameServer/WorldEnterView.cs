using System.Collections.Generic;
using ExitGames.Client.Photon;
using GameCommon;
using UnityEngine;

public class WorldEnterView : MonoBehaviour
{
	private void Start()
	{
		OperationRequest request = new OperationRequest()
		{
			OperationCode = (byte)MessageOperationCode.GameOperationCode,
			Parameters = new Dictionary<byte, object>
			{
				{ PhotonEngine.Instance.SubCodeParameterCode, (int)MessageSubCode.WorldEnterSubCode },
			}
		};

		PhotonEngine.Instance.SendRequest(request);
	}
}