namespace GameCommon
{
	/// <summary>
	/// Sub codes to send to the server/client.
	/// </summary>
    public enum MessageSubCode
    {
		// login
		RegisterSubCode = 0,
		LoginSubCode = 1,

		// Chat
		TestChatSubCode = 2,

		// Game
		TestGameSubCode = 3
    }
}
