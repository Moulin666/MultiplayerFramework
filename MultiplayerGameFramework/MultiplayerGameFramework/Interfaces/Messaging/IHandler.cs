using MultiplayerGameFramework.Implementation.Messaging;

namespace MultiplayerGameFramework.Interfaces.Messaging
{
	/// <summary>
	/// Message handler.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public interface IHandler<T>
	{
		/// <summary>
		/// Message type for handler.
		/// </summary>
		MessageType Type { get; }
		/// <summary>
		/// GameCommon.OperationCode.
		/// </summary>
		byte Code { get; }
		/// <summary>
		/// GameCommon.SubCode.
		/// </summary>
		int? SubCode { get; }

		/// <summary>
		/// Handling operation.
		/// </summary>
		/// <param name="message">Message from client.</param>
		/// <param name="peer">Client.</param>
		/// <returns>bool with handle state.</returns>
		bool HandleMessage(IMessage message, T peer);
	}
}
