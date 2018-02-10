using Autofac;
using Servers.BackgroundThreads;
using Servers.Config;
using Servers.Handlers.ProxyServerHandlers;
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
			builder.RegisterType<LoginProxyHandler>().AsImplementedInterfaces();
			builder.RegisterType<ChatProxyHandler>().AsImplementedInterfaces();
			builder.RegisterType<GameProxyHandler>().AsImplementedInterfaces();
		}
	}
}
