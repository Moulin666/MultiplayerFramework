using ExitGames.Logging;
using MultiplayerGameFramework;
using MultiplayerGameFramework.Interfaces.Support;
using Photon.SocketServer;
using MultiplayerGameFramework.Implementation.Config;
using Photon.SocketServer.ServerToServer;
using ExitGames.Logging.Log4Net;
using log4net;
using LogManager = ExitGames.Logging.LogManager;
using log4net.Config;
using System.IO;
using Autofac;
using Autofac.Configuration;
using Microsoft.Extensions.Configuration;

namespace MGF_Photon
{
	public class PhotonApplication : ApplicationBase
	{
		public ILogger Log { get; set; }

		public IServerApplication ServerApplication { get; set; }
		public IPeerFactory PeerFactory { get; set; }

		protected override PeerBase CreatePeer(InitRequest initRequest)
		{
			var peerConfig = new PeerConfig();
			peerConfig.AddConfig(initRequest);
			return PeerFactory.CreatePeer<PeerBase>(peerConfig);
		}

		protected override S2SPeerBase CreateServerPeer(InitResponse initResponse, object state)
		{
			var peerInfo = state as PeerInfo;
			var peerConfig = new PeerConfig();

			peerConfig.AddConfig(initResponse);
			peerConfig.AddConfig(peerInfo);

			return PeerFactory.CreatePeer<S2SPeerBase>(peerConfig);
		}

		protected override void Setup()
		{
            // Set up LOG
			LogManager.SetLoggerFactory(Log4NetLoggerFactory.Instance);
			Log = LogManager.GetCurrentClassLogger();
			GlobalContext.Properties["LogFileName"] = ApplicationName;
			XmlConfigurator.ConfigureAndWatch(new FileInfo(Path.Combine(BinaryPath, "log4net.config")));

            // Set up CONFIG
			var config = new ConfigurationBuilder();
			config.AddXmlFile(Path.Combine(BinaryPath, ApplicationName + ".config"));
			Log.Info(Path.Combine(BinaryPath, ApplicationName + ".config"));

            // Set up AUTOFAC
            var module = new ConfigurationModule(config.Build());
			var builder = new ContainerBuilder();
			builder.RegisterInstance(this).SingleInstance();
			builder.RegisterInstance(Log).As<ILogger>().SingleInstance();
			builder.RegisterModule(module);

			var container = builder.Build();
			ServerApplication = container.Resolve<IServerApplication>();
			ServerApplication.Setup();
			PeerFactory = container.Resolve<IPeerFactory>();
		}

		protected override void TearDown()
		{
			ServerApplication.TearDown();
		}

		protected override void OnStopRequested()
		{
			ServerApplication.TearDown();
			base.OnStopRequested();
		}
	}
}
