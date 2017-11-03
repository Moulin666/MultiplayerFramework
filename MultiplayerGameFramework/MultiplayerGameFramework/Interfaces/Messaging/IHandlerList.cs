namespace MultiplayerGameFramework.Interfaces.Messaging
{
	public interface IHandlerList<T>
	{
		bool RegisterHandler(IHandler<T> handler);
		bool HandleMessage(IMessage message, T peer);
	}
}
