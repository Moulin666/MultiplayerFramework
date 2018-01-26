namespace MultiplayerGameFramework.Interfaces.Support
{
	/// <summary>
	/// BackgroundThreads interface.
	/// </summary>
	public interface IBackgroundThread
	{
		/// <summary>
		/// Setup backgroundthread.
		/// </summary>
		/// <param name="server">Background thread server.</param>
		void Setup(IServerApplication server);
		/// <summary>
		/// Run backgroundthread.
		/// </summary>
		/// <param name="threadContext"></param>
		void Run(object threadContext);
		/// <summary>
		/// Stop backgroundthread.
		/// </summary>
		void Stop();
	}
}
