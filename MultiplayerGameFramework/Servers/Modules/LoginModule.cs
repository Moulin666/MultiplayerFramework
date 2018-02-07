using Autofac;
using Servers.Handlers;

namespace Servers.Modules
{
	public class LoginModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			base.Load(builder);

			builder.RegisterType<RegisterAccountHandler>().AsImplementedInterfaces();
		}
	}
}
