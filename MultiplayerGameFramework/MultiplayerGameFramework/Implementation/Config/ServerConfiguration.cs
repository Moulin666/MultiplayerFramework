using System;

namespace MultiplayerGameFramework.Implementation.Config
{
	/// <summary>
	/// Server configuration.
	/// </summary>
	public class ServerConfiguration
	{
		/// <summary>
		/// Server Allow Physical Clients?
		/// </summary>
		public bool AllowPhysicalClients { get; set; }

		public int ParentPort { get; set; }
		public int SiblingPort { get; set; }
		/// <summary>
		/// GameCommon.SubCode.
		/// </summary>
		public byte SubCodeParameterCode { get; set; }
		/// <summary>
		/// Client identificator.
		/// </summary>
		public byte PeerIdCode { get; set; }

		public string PublicIpAdress { get; set; }
		public int? TcpPort { get; set; }
		public int? UdpPort { get; set; }

		public Guid? ServerId { get; set; }
		/// <summary>
		/// Id server of type.
		/// </summary>
		public int ServerType { get; set; }
		public string ServerName { get; set; }

		/// <summary>
		/// Base constructor.
		/// </summary>
		/// <param name="allowPhysicalClients"></param>
		/// <param name="parentPort"></param>
		/// <param name="siblingPort"></param>
		/// <param name="subCodeParameterCode">GameCommon.SubCode.</param>
		/// <param name="peerIdCode">Client identificator.</param>
		public ServerConfiguration(bool allowPhysicalClients, int parentPort, int siblingPort, byte subCodeParameterCode, byte peerIdCode)
		{
			AllowPhysicalClients = allowPhysicalClients;
			ParentPort = parentPort;
			SiblingPort = siblingPort;
			SubCodeParameterCode = subCodeParameterCode;
			PeerIdCode = peerIdCode;
		}
	}
}
