using System;

namespace MultiplayerGameFramework.Implementation.Messaging
{
	[Flags]
	public enum MessageType
	{
		Request = 0x1,
		Response = 0x2,
		/// <summary>
		/// Event.
		/// </summary>
		Async = 0x4
	}
}
