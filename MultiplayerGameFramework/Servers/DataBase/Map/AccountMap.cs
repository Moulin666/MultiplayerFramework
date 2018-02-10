using FluentNHibernate.Mapping;
using Servers.DataBase.Model;

namespace Servers.DataBase.Map
{
	public class AccountMap : ClassMap<AccountModel>
	{
		public AccountMap()
		{
			Id(x => x.Id).Column("id");

			Map(x => x.Login).Column("login");
			Map(x => x.Password).Column("password");
			Map(x => x.Salt).Column("salt");
			Map(x => x.Email).Column("email");

			Map(x => x.Created).Column("created_at");
			Map(x => x.Updated).Column("updated_at");

			Table("fw_accounts");
		}
	}
}
