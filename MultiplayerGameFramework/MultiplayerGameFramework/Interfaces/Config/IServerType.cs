namespace MultiplayerGameFramework.Interfaces.Config
{
	/// <summary>
	/// Type of the server.
	/// </summary>
	public interface IServerType
	{
		/// <summary>
		/// Server name.
		/// </summary>
		string Name { get; }

		/// <summary>
		/// Get server type by server ID.
		/// </summary>
		/// <param name="serverType">Server ID.</param>
		/// <returns>ServerType, IServerType.</returns>
		IServerType GetServerType(int serverType);
	}
}
