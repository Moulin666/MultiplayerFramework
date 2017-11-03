namespace MultiplayerGameFramework.Interfaces.Config
{
	public interface IServerType
	{
		string Name { get; }

		IServerType GetServerType(int serverType);
	}
}
