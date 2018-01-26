namespace MultiplayerGameFramework.Interfaces.Config
{
	/// <summary>
	/// Peer configuration.
	/// </summary>
	public interface IPeerConfig
	{
		/// <summary>
		/// Get Configuration.
		/// </summary>
		/// <typeparam name="T">class.</typeparam>
		/// <returns>T configuration.</returns>
		T GetConfig<T>() where T : class;

		/// <summary>
		/// Add new configuration.
		/// </summary>
		/// <param name="obj">Configuration for adding.</param>
		void AddConfig(object obj);
	}
}
