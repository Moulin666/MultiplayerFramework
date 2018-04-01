using MultiplayerGameFramework.Interfaces.Config;

namespace Servers.Config
{
	public class ServerType : IServerType
	{
		#region Servers

		public static ServerType ProxyServer = new ServerType() { Name = "Proxy" };
		public static ServerType LoginServer = new ServerType() { Name = "Login" };
		public static ServerType ChatServer = new ServerType() { Name = "Chat" };
		public static ServerType GameServer = new ServerType() { Name = "Game" };

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

				case 2:
					server = ChatServer;
					break;

				case 3:
					server = GameServer;
					break;

				case 0:
				default:
					server = ProxyServer;
					break;
			}
			
			return server;
		}
	}
}
