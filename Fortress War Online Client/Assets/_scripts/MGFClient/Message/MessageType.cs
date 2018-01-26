public enum MessageType
{
	// Request = 0x1, // summary Request to the server.
	
	/// <summary>
	/// Response from the server.
	/// </summary>
	Response = 0x2,

	/// <summary>
	/// Event from the server.
	/// </summary>
	Async = 0x4
}
