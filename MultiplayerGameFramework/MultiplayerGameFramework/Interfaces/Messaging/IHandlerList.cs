namespace MultiplayerGameFramework.Interfaces.Messaging
{
	/// <summary>
	/// List of handlers.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public interface IHandlerList<T>
	{
		/// <summary>
		/// Register new handler.
		/// </summary>
		/// <param name="handler">IHandler for register.</param>
		/// <returns>Register state.</returns>
		bool RegisterHandler(IHandler<T> handler);
		/// <summary>
		/// Handle message.
		/// </summary>
		/// <param name="message">Message from client.</param>
		/// <param name="peer">Client.</param>
		/// <returns>Handle state.</returns>
		bool HandleMessage(IMessage message, T peer);
	}
}
