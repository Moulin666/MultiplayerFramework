using FluentNHibernate.Mapping;
using Servers.DataBase.Model;

namespace Servers.DataBase.Map
{
	public class CharacterMap : ClassMap<CharacterModel>
	{
		public CharacterMap()
		{
			Id(x => x.Id).Column("id");

			References(x => x.AccountId).Column("account_id");

			Map(x => x.Name).Column("name");
			Map(x => x.Sex).Column("sex");
			Map(x => x.CharacterType).Column("type");
			Map(x => x.Class).Column("class");
			Map(x => x.SubClass).Column("sub_class");

			Map(x => x.Level).Column("level");
			Map(x => x.Exp).Column("exp");
			Map(x => x.Strength).Column("strength");
			Map(x => x.Intellect).Column("intellect");

			Map(x => x.RangLevel).Column("rang_level");

			Map(x => x.Gold).Column("gold");
			Map(x => x.Donate).Column("donate");
			Map(x => x.SkillPoint).Column("skill_point");
			Map(x => x.StatPoint).Column("stat_point");

			Map(x => x.InventorySize).Column("inventory_size");

			//References(x => x.GuildId).Column("guild_id");
			Map(x => x.GuildId).Column("guild_id");

			Map(x => x.Created).Column("created_at");
			Map(x => x.Updated).Column("updated_at");

			Table("fw_characters");
		}
	}
}
