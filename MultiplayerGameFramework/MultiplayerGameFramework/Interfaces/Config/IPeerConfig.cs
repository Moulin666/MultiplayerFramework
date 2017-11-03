namespace MultiplayerGameFramework.Interfaces.Config
{
	public interface IPeerConfig
	{
		T GetConfig<T>() where T : class;

		void AddConfig(object obj);
	}
}
