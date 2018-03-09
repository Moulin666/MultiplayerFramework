namespace GameCommon
{
	/// <summary>
	/// Operation code to send to the server/client operation.
	/// </summary>
    public enum MessageOperationCode : byte
    {
		LoginOperationCode = 1,
		ChatOperationCode,
		GameOperationCode,
    }
}
