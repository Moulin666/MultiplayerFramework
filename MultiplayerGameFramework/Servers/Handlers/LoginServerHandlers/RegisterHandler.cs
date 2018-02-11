using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;
using ExitGames.Logging;
using GameCommon;
using GameCommon.MessageObjects;
using MGF_Photon.Implementation.Server;
using MultiplayerGameFramework.Implementation.Messaging;
using MultiplayerGameFramework.Interfaces.Messaging;
using MultiplayerGameFramework.Interfaces.Server;
using Servers.DataBase;
using Servers.DataBase.Model;
using Servers.Handlers.LoginServerHandlers.Operations;

namespace Servers.Handlers
{
	public class RegisterHandler : IHandler<IServerPeer>
	{
		public ILogger Log { get; set; }

		public RegisterHandler(ILogger log)
		{
			Log = log;
		}

		public MessageType Type => MessageType.Request;

		public byte Code => (byte)MessageOperationCode.LoginOperationCode;

		public int? SubCode => (int)MessageSubCode.RegisterSubCode;

		public bool HandleMessage(IMessage message, IServerPeer peer)
		{
			var serverPeer = peer as PhotonServerPeer;
			var operation = new RegisterOperation(serverPeer.protocol, message);

			if (!operation.IsValid)
			{
				peer.SendMessage(new Response(Code, SubCode, new Dictionary<byte, object>()
				{
					{ (byte)MessageParameterCode.SubCodeParameterCode, SubCode },
					{ (byte)MessageParameterCode.PeerIdParameterCode, message.Parameters[(byte)MessageParameterCode.PeerIdParameterCode] },
				}, operation.GetErrorMessage(), (int)ReturnCode.OperationInvalid));

				return true;
			}

			if (operation.Login.Length < 6 || operation.Password.Length < 6)
			{
				peer.SendMessage(new Response(Code, SubCode, new Dictionary<byte, object>()
				{
					{ (byte)MessageParameterCode.SubCodeParameterCode, SubCode },
					{ (byte)MessageParameterCode.PeerIdParameterCode, message.Parameters[(byte)MessageParameterCode.PeerIdParameterCode] },
				}, "Login and password can't be less than 6 symbols.", (int)ReturnCode.OperationInvalid));

				return true;
			}

			var checkMail = new EmailAddressAttribute();
			if (!checkMail.IsValid(operation.Email))
			{
				peer.SendMessage(new Response(Code, SubCode, new Dictionary<byte, object>()
				{
					{ (byte)MessageParameterCode.SubCodeParameterCode, SubCode },
					{ (byte)MessageParameterCode.PeerIdParameterCode, message.Parameters[(byte)MessageParameterCode.PeerIdParameterCode] },
				}, "Email address incorrect.", (int)ReturnCode.OperationInvalid));

				return true;
			}

			var characterData = MessageSerializerService.DeserializeObjectOfType<RegisterCharacterData>(
				operation.CharacterRegisterData);

			if (characterData.CharacterName.Length < 6)
			{
				peer.SendMessage(new Response(Code, SubCode, new Dictionary<byte, object>()
				{
					{ (byte)MessageParameterCode.SubCodeParameterCode, SubCode },
					{ (byte)MessageParameterCode.PeerIdParameterCode, message.Parameters[(byte)MessageParameterCode.PeerIdParameterCode] },
				}, "Name can't be less than 6 symbols.", (int)ReturnCode.OperationInvalid));

				return true;
			}
			else if (characterData.Sex != "Male" && characterData.Sex != "Female")
				return true;
			else if (characterData.CharacterType != 1) // add more types
				return true;
			else if (characterData.Class != "Warrior" && characterData.Class != "Rogue" && characterData.Class != "Mage")
				return true;
			else if (characterData.SubClass != "Warlock" && characterData.SubClass != "Cleric")
				return true;

			try
			{
				using (var session = NHibernateHelper.OpenSession())
				{
					using (var transaction = session.BeginTransaction())
					{
						var accounts = session.QueryOver<AccountModel>().Where(a => a.Login == operation.Login).List();
						var characters = session.QueryOver<CharacterModel>().Where(c => c.Name == characterData.CharacterName).List();

						if (accounts.Count > 0)
						{
							transaction.Commit();

							peer.SendMessage(new Response(Code, SubCode, new Dictionary<byte, object>()
							{
								{ (byte)MessageParameterCode.SubCodeParameterCode, SubCode },
								{ (byte)MessageParameterCode.PeerIdParameterCode, message.Parameters[(byte)MessageParameterCode.PeerIdParameterCode] },
							}, "Login already taken.", (int)ReturnCode.AlreadyExist));

							return true;
						}
						else if (characters.Count > 0)
						{
							transaction.Commit();

							peer.SendMessage(new Response(Code, SubCode, new Dictionary<byte, object>()
							{
								{ (byte)MessageParameterCode.SubCodeParameterCode, SubCode },
								{ (byte)MessageParameterCode.PeerIdParameterCode, message.Parameters[(byte)MessageParameterCode.PeerIdParameterCode] },
							}, "Name already taken.", (int)ReturnCode.AlreadyExist));

							return true;
						}

						string salt = Guid.NewGuid().ToString().Replace("-", "");

						AccountModel newAccount = new AccountModel()
						{
							Login = operation.Login,
							Password = BitConverter.ToString(SHA512.Create().ComputeHash(
								Encoding.UTF8.GetBytes(salt + operation.Password))).Replace("-", ""),
							Salt = salt,
							Email = operation.Email,

							AdminLevel = 0,
							BanLevel = 0,

							Created = DateTime.Now,
							Updated = DateTime.Now
						};

						session.Save(newAccount);
						transaction.Commit();

						Log.DebugFormat("Create new Account. Login - {0}.", operation.Login);
					}

					using (var transaction = session.BeginTransaction())
					{
						var accounts = session.QueryOver<AccountModel>().Where(a => a.Login == operation.Login).SingleOrDefault();

						CharacterModel newCharacter = new CharacterModel()
						{
							AccountId = accounts,

							Name = characterData.CharacterName,
							Sex = characterData.Sex,
							CharacterType = characterData.CharacterType,
							Class = characterData.Class,
							SubClass = characterData.SubClass,

							Level = 1,
							Exp = 0,
							Strength = 1,
							Intellect = 1,

							RangLevel = 0,

							Gold = 10000,
							Donate = 0,
							SkillPoint = 1000,
							StatPoint = 0,

							InventorySize = 32,

							GuildId = 0,

							Created = DateTime.Now,
							Updated = DateTime.Now
						};

						session.Save(newCharacter);
						transaction.Commit();

						Log.DebugFormat("Create new Character. Name - {0}.", characterData.CharacterName);
					}

					peer.SendMessage(new Response(Code, SubCode, new Dictionary<byte, object>()
					{
						{ (byte)MessageParameterCode.SubCodeParameterCode, SubCode },
						{ (byte)MessageParameterCode.PeerIdParameterCode, message.Parameters[(byte)MessageParameterCode.PeerIdParameterCode] },
					}, "Register success.", (int)ReturnCode.OK));
				}

				return true;
			}
			catch (Exception ex)
			{
				Log.ErrorFormat("Error register handler: {0}", ex);

				peer.SendMessage(new Response(Code, SubCode, new Dictionary<byte, object>()
				{
					{ (byte)MessageParameterCode.SubCodeParameterCode, SubCode },
					{ (byte)MessageParameterCode.PeerIdParameterCode, message.Parameters[(byte)MessageParameterCode.PeerIdParameterCode] },
				}, ex.ToString(), (int)ReturnCode.OperationDenied));

				return true;
			}
		}
	}
}
