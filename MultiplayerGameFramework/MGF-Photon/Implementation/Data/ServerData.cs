using MultiplayerGameFramework.Interfaces.Server;
using System;

namespace MGF_Photon.Implementation.Data
{
	public class ServerData : IServerData
	{
		public Guid? ServerId { get; set; }

		public string TcpAddress { get; set; }
		public string UdpAddress { get; set; }
		public string ApplicationName { get; set; }

		public int ServerType { get; set; }
	}
}
