using Autofac;
using Servers.BackgroundThreads;
using Servers.Config;
using Servers.Handlers.ChatServerHandlers;
using Servers.Handlers.GameServerHandlers;
using Servers.Handlers.LoginServerHandlers;
using Servers.Support;

namespace Servers.Modules
{
	public class ProxyModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			base.Load(builder);

			// What we need
			builder.RegisterType<ClientCodeRemover>().AsImplementedInterfaces();
			builder.RegisterType<ServerType>().AsImplementedInterfaces();

			// Background Threads
			builder.RegisterType<TestBackgroundThread>().AsImplementedInterfaces();

			// Handlers
			builder.RegisterType<ClientLoginForwardingRequestHandler>().AsImplementedInterfaces();
			builder.RegisterType<ClientChatForwardingRequestHandler>().AsImplementedInterfaces();
			builder.RegisterType<ClientGameForwardingRequestHandler>().AsImplementedInterfaces();
		}
	}
}
