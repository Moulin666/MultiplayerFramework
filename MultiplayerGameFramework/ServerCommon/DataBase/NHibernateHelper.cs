using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;

namespace ServerCommon.DataBase
{
	public class NHibernateHelper
	{
		public NHibernateHelper()
		{
			InitializeSessionFactory();
		}

		private static ISessionFactory _sessionFactory;

		public static ISessionFactory SessionFactory
		{
			get
			{
				if (_sessionFactory == null)
					InitializeSessionFactory();

				return _sessionFactory;
			}
		}

		private static void InitializeSessionFactory()
		{
			_sessionFactory = Fluently.Configure().Database(
				MySQLConfiguration.Standard
				.ConnectionString(fw => fw.Server("localhost")
				.Database("fwo")
				.Username("Root")
				.Password("325862123")))
				.Mappings(m => m.FluentMappings.AddFromAssemblyOf<NHibernateHelper>())
				.BuildSessionFactory();
		}

		public static ISession OpenSession()
		{
			return _sessionFactory.OpenSession();
		}
	}
}
