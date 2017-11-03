namespace MultiplayerGameFramework.Interfaces.Support
{
	public interface IBackgroundThread
	{
		void Setup(IServerApplication server);
		void Run(object threadContext);
		void Stop();
	}
}
