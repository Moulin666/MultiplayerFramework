using Autofac;
using Servers.Handlers;

namespace Servers.Modules
{
	public class GameModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			base.Load(builder);

			builder.RegisterType<WorldEnterHandler>().AsImplementedInterfaces();
		}
	}
}
