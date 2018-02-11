using System;

namespace GameCommon.MessageObjects
{
	[Serializable]
	public class RegisterCharacterData
	{
		public string CharacterName { get; set; }
		public string Sex { get; set; }
		public int CharacterType { get; set; }
		public string Class { get; set; }
		public string SubClass { get; set; }
	}
}
