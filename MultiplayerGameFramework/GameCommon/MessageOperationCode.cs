namespace GameCommon
{
	/// <summary>
	/// Operation code to send to the server/client operation.
	/// </summary>
    public enum MessageOperationCode : byte
    {
		LoginOperationCode = 0,
		ChatOperationCode = 1,
		GameOperationCode = 2,
    }
}
