using Autofac;
using Servers.Handlers;

namespace Servers.Modules
{
	public class ChatModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			base.Load(builder);

			builder.RegisterType<ChatRegisterHandler>().AsImplementedInterfaces();
			builder.RegisterType<ChatDeregisterHandler>().AsImplementedInterfaces();
		}
	}
}
