using Autofac;
using Servers.BackgroundThreads;
using Servers.Handlers;

namespace Servers.Modules
{
	public class TestModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			base.Load(builder);

			builder.RegisterType<TestRequestResponseHandler>().AsImplementedInterfaces();
			builder.RegisterType<TestRequestEventHandler>().AsImplementedInterfaces();
			builder.RegisterType<TestBackgroundThread>().AsImplementedInterfaces();
		}
	}
}
