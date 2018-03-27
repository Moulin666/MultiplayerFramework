using ExitGames.Logging;
using MultiplayerGameFramework;
using MultiplayerGameFramework.Implementation.Config;
using MultiplayerGameFramework.Interfaces.Client;
using MultiplayerGameFramework.Interfaces.Config;
using MultiplayerGameFramework.Interfaces.Server;
using MultiplayerGameFramework.Interfaces.Support;
using System.Collections.Generic;
using System.Threading;
using System.Xml.Serialization;
using System.IO;
using Photon.SocketServer;
using MGF_Photon.Implementation.Operation.Data;
using MGF_Photon.Implementation.Operation;
using MGF_Photon.Implementation.Server;

namespace MGF_Photon.Implementation
{
	public class ServerApplication : IServerApplication
	{
		public ServerConfiguration ServerConfiguration { get; set; }

		protected ILogger Log;
		protected PhotonApplication Server;

		private readonly IEnumerable<PeerInfo> _peerInfo;
		private readonly IEnumerable<IBackgroundThread> _backgroundThreads;
		private readonly IServerConnectionCollection<IServerType, IServerPeer> _serverCollection;
		private readonly IConnectionCollection<IClientPeer> _clientCollection;
		private readonly IEnumerable<IAfterServerRegistration> _afterServerRegistrationEvents;

		/// <summary>
		/// Sub code for the server. GameCommon.SubCode.
		/// </summary>
		public byte SubCodeParameterCode { get { return ServerConfiguration.SubCodeParameterCode; } }
		/// <summary>
		/// Path to the server.
		/// </summary>
		public string BinaryPath { get { return Server.BinaryPath; } }
		/// <summary>
		/// Application Name from configuration.
		/// </summary>
		public string ApplicationName { get { return Server.ApplicationName; } }

		/// <summary>
		/// Base constructor.
		/// </summary>
		/// <param name="serverConfiguration"></param>
		/// <param name="log"></param>
		/// <param name="server"></param>
		/// <param name="peerInfo"></param>
		/// <param name="backgroundThreads"></param>
		/// <param name="serverCollection"></param>
		/// <param name="clientCollection"></param>
		/// <param name="afterServerRegistrationEvents"></param>
		public ServerApplication(ServerConfiguration serverConfiguration,
			ILogger log,
			PhotonApplication server,
			IEnumerable<PeerInfo> peerInfo,
			IEnumerable<IBackgroundThread> backgroundThreads,
			IServerConnectionCollection<IServerType, IServerPeer> serverCollection,
			IConnectionCollection<IClientPeer> clientCollection,
			IEnumerable<IAfterServerRegistration> afterServerRegistrationEvents)
		{
			ServerConfiguration = serverConfiguration;
			Log = log;
			Server = server;
			_peerInfo = peerInfo;
			_backgroundThreads = backgroundThreads;
			_serverCollection = serverCollection;
			_clientCollection = clientCollection;
			_afterServerRegistrationEvents = afterServerRegistrationEvents;
		}

		/// <summary>
		/// Setup server.
		/// </summary>
		public void Setup()
		{
			// Start Background Threads
			foreach (var backgroundThread in _backgroundThreads)
			{
				backgroundThread.Setup(this);
				ThreadPool.QueueUserWorkItem(backgroundThread.Run);
			}
            Log.Debug("Setup");
			// Start servers
			foreach (var peerInfo in _peerInfo)
			{
				ConnectToPeer(peerInfo);
                Log.Info("Connecting server");
			}
		}

		/// <summary>
		/// TearDown server.
		/// </summary>
		public void TearDown()
		{
			// Disconnect clients
			var clients = _clientCollection.GetPeers<IClientPeer>();
			Log.DebugFormat("Disconnecting {0} peers", clients.Count);

			foreach (var client in clients)
			{
				client.Disconnect();
			}
			_clientCollection.Clear();

			// Disconnect servers
			var servers = _serverCollection.GetPeers<IServerPeer>();
			Log.DebugFormat("Disconnecting {0} servers", servers.Count);

			foreach (var server in servers)
			{
				server.Disconnect();
			}
			_serverCollection.Clear();

			// Stop Background Threads
			foreach(var backgroundThread in _backgroundThreads)
			{
				backgroundThread.Stop();
			}
		}

		/// <summary>
		/// Failed connection.
		/// </summary>
		/// <param name="errorCode">Code with error.</param>
		/// <param name="errorMessage">Error message.</param>
		/// <param name="state">State object.</param>
		public void OnServerConnectionFailed(int errorCode, string errorMessage, object state)
		{
			Log.InfoFormat("[ServerApplication]: ServerConnectionFailed({0}) - {1}", errorCode, errorMessage);
		}

		/// <summary>
		/// Do somthing after server registration
		/// </summary>
		/// <param name="serverPeer">Server Peer.</param>
		public void AfterServerRegistration(IServerPeer serverPeer)
		{
			foreach(var afterServerRegistration in _afterServerRegistrationEvents)
			{
				Log.DebugFormat("After - {0}", afterServerRegistration);
				afterServerRegistration.AfterRegister(serverPeer);
			}
		}

		/// <summary>
		/// Do connect to the peer.
		/// </summary>
		/// <param name="peerInfo">MGF.Implementation.PeerInfo.</param>
		public void ConnectToPeer(PeerInfo peerInfo)
		{
			var outbound = new OutboundPhotonPeer(Server, peerInfo);

			if(outbound.ConnectTcp(peerInfo.MasterEndPoint, peerInfo.ApplicationName, TypeCache.SerializePeerInfo(peerInfo)) == false)
			{
				Log.Warn("Connection refused");
			}
		}

		/// <summary>
		/// Do reconnect to the peer.
		/// </summary>
		/// <param name="peerInfo">MGF.Implementation.PeerInfo.</param>
		public void ReconnectToPeer(PeerInfo peerInfo)
		{
			peerInfo.NumTries++;
			if(peerInfo.MaxTries > peerInfo.NumTries)
			{
				var timer = new Timer(o => ConnectToPeer(peerInfo), null, peerInfo.ConnectRetryIntervalSeconds * 1000, 0);
			}
		}

		/// <summary>
		/// Do register server.
		/// </summary>
		/// <param name="peer">MGF.Implementation.Server.PhotonServerPeer</param>
		public void Register(PhotonServerPeer peer)
		{
			var registerSubServerOperation = new RegisterSubServerData()
			{
				GameServerAddress = ServerConfiguration.PublicIpAdress,
				TcpPort = ServerConfiguration.TcpPort,
				UdpPort = ServerConfiguration.UdpPort,
				ServerId = ServerConfiguration.ServerId,
				ServerType = ServerConfiguration.ServerType,
				ServerName = ServerConfiguration.ServerName
			};

			XmlSerializer mySerializer = new XmlSerializer(typeof(RegisterSubServerData));
			StringWriter outString = new StringWriter();

			mySerializer.Serialize(outString, registerSubServerOperation);

			peer.SendOperationRequest(new OperationRequest(0, new RegisterSubServer() { RegisterSubServerOperation = outString.ToString() }), new SendParameters());
		}
	}
}
