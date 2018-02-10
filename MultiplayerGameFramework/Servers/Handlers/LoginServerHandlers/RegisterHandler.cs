using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using ExitGames.Logging;
using GameCommon;
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

			try
			{
				using (var session = NHibernateHelper.OpenSession())
				{
					using (var transaction = session.BeginTransaction())
					{
						var accounts = session.QueryOver<AccountModel>().Where(a => a.Login == operation.Login).List();

						if (accounts.Count > 0)
						{
							transaction.Commit();

							peer.SendMessage(new Response(Code, SubCode, new Dictionary<byte, object>()
							{
								{ (byte)MessageParameterCode.SubCodeParameterCode, SubCode },
								{ (byte)MessageParameterCode.PeerIdParameterCode, message.Parameters[(byte)MessageParameterCode.PeerIdParameterCode] },
							}, "Login already taken.", (int)ReturnCode.AlreadyExist));
						}

						string salt = Guid.NewGuid().ToString().Replace("-", "");

						AccountModel newAccount = new AccountModel()
						{
							Email = operation.Email,
							Login = operation.Login,
							Password = BitConverter.ToString(SHA512.Create().ComputeHash(
								Encoding.UTF8.GetBytes(salt + operation.Password))).Replace("-", ""),
							Salt = salt,
							Created = DateTime.Now,
							Updated = DateTime.Now
						};

						session.Save(newAccount);
						transaction.Commit();

						Log.DebugFormat("Create new Account. Login - {0}", operation.Login);
					}

					//using (var transaction = session.BeginTransaction())
					//{
					//	var accounts = session.QueryOver<AccountModel>().Where(a => a.Login == operation.Login).List();

					//	if (accounts.Count > 0)
					//	{
					//		CharacterModel newCharacter = new CharacterModel()
					//		{
					//			AccountId = accounts[0],
					//			Name = operation.
					//		};

					//		session.Save(newCharacter);
					//		transaction.Commit();

					//		Log.DebugFormat("Create new Character. CharacterName - {0}", );
					//	} 
					//}

					peer.SendMessage(new Response(Code, SubCode, new Dictionary<byte, object>()
					{
						{ (byte)MessageParameterCode.SubCodeParameterCode, SubCode },
						{ (byte)MessageParameterCode.PeerIdParameterCode, message.Parameters[(byte)MessageParameterCode.PeerIdParameterCode] },
					}, "Register success", (int)ReturnCode.OK));
				}
			}
			catch (Exception ex)
			{
				Log.ErrorFormat("Error register handler: {0}", ex);

				peer.SendMessage(new Response(Code, SubCode, new Dictionary<byte, object>()
				{
					{ (byte)MessageParameterCode.SubCodeParameterCode, SubCode },
					{ (byte)MessageParameterCode.PeerIdParameterCode, message.Parameters[(byte)MessageParameterCode.PeerIdParameterCode] },
				}, ex.ToString(), (int)ReturnCode.OperationDenied));
			}

			return true;
		}
	}
}
