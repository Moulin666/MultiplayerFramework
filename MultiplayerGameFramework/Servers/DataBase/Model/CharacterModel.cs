using System;

namespace Servers.DataBase.Model
{
	public class CharacterModel
	{
		public virtual int Id { get; set; }
		public virtual AccountModel AccountId { get; set; }
		
		public virtual string Name { get; set; }
		public virtual string Sex { get; set; }
		public virtual int CharacterType { get; set; }
		public virtual int CharacterHeight { get; set; }
		public virtual string Class { get; set; }
		public virtual string SubClass { get; set; }

		//public virtual int Level { get; set; }
		//public virtual long Exp { get; set; }
		//public virtual int Strength { get; set; }
		//public virtual int Intellect { get; set; }

		//public virtual int RangLevel { get; set; }

		//public virtual long Gold { get; set; }
		//public virtual long SkillPoint { get; set; }
		//public virtual int StatPoint { get; set; }

		//public long Health { get; set; }
		//public long Mana { get; set; }

		//public virtual int InventorySize { get; set; }

		//public virtual GuildModel GuildId

		//public virtual int AdminLevel { get; set; }
		//public virtual int BanLevel { get; set; }

		public virtual DateTime Created { get; set; }
		public virtual DateTime Updated { get; set; }
	}
}
