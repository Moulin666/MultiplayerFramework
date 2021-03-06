﻿namespace GameCommon
{
	/// <summary>
	/// Parameter code to the store parameters with them parameter codes to send to the server/client.
	/// </summary>
    public enum MessageParameterCode : byte
    {
        SubCodeParameterCode = 0,
		PeerIdParameterCode,

		TestMessageParameterCode,

		Login,
		Password,
		Email,

		CharacterRegisterData,
		UserId,
	}
}
