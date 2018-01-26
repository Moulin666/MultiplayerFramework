using System.Net;

namespace MultiplayerGameFramework.Implementation.Config
{
	/// <summary>
	/// Peer information.
	/// </summary>
	public class PeerInfo
	{
		/// <summary>
		/// Master IP address and port number.
		/// </summary>
		public IPEndPoint MasterEndPoint { get; set; }
		/// <summary>
		/// Connect retry interval.
		/// </summary>
		public int ConnectRetryIntervalSeconds { get; set; }

		/// <summary>
		/// Is Sibling Connection?
		/// </summary>
		public bool IsSiblingConnection { get; set; }
		/// <summary>
		/// Maximum number tries.
		/// </summary>
		public int MaxTries { get; set; }
		/// <summary>
		/// Current number tries.
		/// </summary>
		public int NumTries { get; set; }

		/// <summary>
		/// Application name our server.
		/// </summary>
		public string ApplicationName { get; set; }

		/// <summary>
		/// Base constructor.
		/// </summary>
		/// <param name="ipAdress"></param>
		/// <param name="ipPort"></param>
		/// <param name="connectRetryIntervalSeconds"></param>
		/// <param name="isSiblingConnection"></param>
		/// <param name="maxTries">Maximum number tries.</param>
		/// <param name="applicationName">Application name our server.</param>
		public PeerInfo(string ipAdress, int ipPort, int connectRetryIntervalSeconds, bool isSiblingConnection, int maxTries, string applicationName)
		{
			MasterEndPoint = new IPEndPoint(IPAddress.Parse(ipAdress), ipPort);
			ConnectRetryIntervalSeconds = connectRetryIntervalSeconds;
			IsSiblingConnection = isSiblingConnection;
			MaxTries = maxTries;
			NumTries = 0;
			ApplicationName = applicationName;
		}
	}
}
