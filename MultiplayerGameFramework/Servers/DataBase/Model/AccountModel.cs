using System;

namespace Servers.DataBase.Model
{
	public class AccountModel
	{
		public virtual int Id { get; set; }
		public virtual string Login { get; set; }
		public virtual string Password { get; set; }
		public virtual string Salt { get; set; }
		public virtual string Email { get; set; }
		
		public virtual DateTime Created { get; set; }
		public virtual DateTime Updated { get; set; }
	}
}
