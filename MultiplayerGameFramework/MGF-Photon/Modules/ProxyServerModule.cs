using Autofac;
using MGF_Photon.Implementation;
using MGF_Photon.Implementation.Client;
using MGF_Photon.Implementation.Data;
using MGF_Photon.Implementation.Handler;
using MGF_Photon.Implementation.Server;
using MultiplayerGameFramework.Implementation.Client;
using MultiplayerGameFramework.Implementation.Config;
using MultiplayerGameFramework.Implementation.Messaging;
using MultiplayerGameFramework.Implementation.Server;

namespace MGF_Photon.Modules
{
	public class ProxyServerModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			base.Load(builder);

			builder.RegisterType<ServerApplication>().AsImplementedInterfaces().SingleInstance();
			builder.RegisterType<PhotonPeerFactory>().AsImplementedInterfaces().SingleInstance();
			builder.RegisterType<PeerConfig>().AsImplementedInterfaces().SingleInstance();
			builder.RegisterType<SubServerClientPeer>().AsSelf().AsImplementedInterfaces();
			builder.RegisterType<PhotonClientPeer>().AsSelf().AsImplementedInterfaces();
			builder.RegisterType<PhotonServerPeer>().AsSelf().AsImplementedInterfaces();
			builder.RegisterType<ServerConnectionCollection>().AsImplementedInterfaces().SingleInstance();
			builder.RegisterType<ClientConnectionCollection>().AsImplementedInterfaces().SingleInstance();
			builder.RegisterType<ServerHandlerList>().AsImplementedInterfaces();
			builder.RegisterType<ClientHandlerList>().AsImplementedInterfaces();
            
			builder.RegisterType<EventForwardHandler>().AsImplementedInterfaces();
			builder.RegisterType<RequestForwarHandler>().AsImplementedInterfaces();
			builder.RegisterType<ResponseForwardHandler>().AsImplementedInterfaces();

			builder.RegisterType<HandleServerRegistration>().AsImplementedInterfaces();
			builder.RegisterType<ServerData>().AsImplementedInterfaces();
		}
	}
}
