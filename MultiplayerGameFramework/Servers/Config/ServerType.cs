using MultiplayerGameFramework.Interfaces.Config;

namespace Servers.Config
{
	public class ServerType : IServerType
	{
		#region Servers

		public static ServerType ProxyServer = new ServerType() { Name = "Proxy" };
		public static ServerType LoginServer = new ServerType() { Name = "Login" };

		#endregion

		public string Name { get; set; }

		public IServerType GetServerType(int serverType)
		{
			IServerType server = null;

			switch(serverType)
			{
				case 1:
					server = LoginServer;
					break;

				// other servers

				case 0:
				default:
					server = ProxyServer;
					break;
			}

			return server;
		}
	}
}
