namespace GameCommon
{
	/// <summary>
	/// Sub codes to send to the server/client.
	/// </summary>
    public enum MessageSubCode
    {
		// login
		RegisterSubCode = 1,
		LoginSubCode,

		// Chat
		TestChatSubCode,

		// Game
		WorldEnterSubCode,
	}
}
