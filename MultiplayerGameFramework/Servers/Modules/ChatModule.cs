using Autofac;
using Servers.Handlers;

namespace Servers.Modules
{
	public class ChatModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			base.Load(builder);

			builder.RegisterType<TestChatHandler>().AsImplementedInterfaces();
		}
	}
}
