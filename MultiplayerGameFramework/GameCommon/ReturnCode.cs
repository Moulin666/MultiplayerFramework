namespace GameCommon
{
	/// <summary>
	/// Return code for client and server
	/// Error code
	/// </summary>
	public enum ReturnCode : short
	{
		OperationDenied = -3,
		OperationInvalid = -2,
		InternalServerError = -1,

		OK = 0,

		AlreadyExist,
		LoginOrPasswordIncorrect,
	}
}
