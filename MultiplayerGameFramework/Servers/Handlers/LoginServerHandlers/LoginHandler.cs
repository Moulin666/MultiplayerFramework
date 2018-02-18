using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using ExitGames.Logging;
using GameCommon;
using MGF_Photon.Implementation.Server;
using MultiplayerGameFramework.Implementation.Client;
using MultiplayerGameFramework.Implementation.Messaging;
using MultiplayerGameFramework.Interfaces.Messaging;
using MultiplayerGameFramework.Interfaces.Server;
using Servers.DataBase;
using Servers.DataBase.Model;
using Servers.Handlers.LoginServerHandlers.Operations;

namespace Servers.Handlers
{
	public class LoginHandler : IHandler<IServerPeer>
	{
		private SubServerClientPeer _clientConnectionCollection;
		public SubServerClientPeer ClientConnectionCollection
		{
			get
			{
				return _clientConnectionCollection;
			}
			set
			{
				_clientConnectionCollection = value;
			}
		}

		public ILogger Log { get; set; }

		public LoginHandler(ILogger log)
		{
			Log = log;
		}

		public MessageType Type => MessageType.Request;

		public byte Code => (byte)MessageOperationCode.LoginOperationCode;

		public int? SubCode => (int)MessageSubCode.LoginSubCode;

		public bool HandleMessage(IMessage message, IServerPeer peer)
		{
			var serverPeer = peer as PhotonServerPeer;
			var operation = new LoginOperation(serverPeer.protocol, message);

			if (!operation.IsValid)
			{
				Response response = new Response(Code, SubCode, new Dictionary<byte, object>()
				{
					{ (byte)MessageParameterCode.SubCodeParameterCode, SubCode },
					{ (byte)MessageParameterCode.PeerIdParameterCode, message.Parameters[(byte)MessageParameterCode.PeerIdParameterCode] },
				}, operation.GetErrorMessage(), (int)ReturnCode.OperationInvalid);

				peer.SendMessage(response);

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

			try
			{
				using (var session = NHibernateHelper.OpenSession())
				{
					using (var transaction = session.BeginTransaction())
					{
						var account = session.QueryOver<AccountModel>().Where(a => a.Login == operation.Login).SingleOrDefault();

						if (account == null)
						{
							transaction.Commit();

							peer.SendMessage(new Response(Code, SubCode, new Dictionary<byte, object>()
							{
								{ (byte)MessageParameterCode.SubCodeParameterCode, SubCode },
								{ (byte)MessageParameterCode.PeerIdParameterCode, message.Parameters[(byte)MessageParameterCode.PeerIdParameterCode] },
							}, "Login or password incorrect.", (int)ReturnCode.LoginOrPasswordIncorrect));

							return true;
						}

						string Password = BitConverter.ToString(SHA512.Create().ComputeHash(
								Encoding.UTF8.GetBytes(account.Salt + operation.Password))).Replace("-", "");

						if (Password != account.Password)
						{
							transaction.Commit();

							peer.SendMessage(new Response(Code, SubCode, new Dictionary<byte, object>()
							{
								{ (byte)MessageParameterCode.SubCodeParameterCode, SubCode },
								{ (byte)MessageParameterCode.PeerIdParameterCode, message.Parameters[(byte)MessageParameterCode.PeerIdParameterCode] },
							}, "Login or password incorrect.", (int)ReturnCode.LoginOrPasswordIncorrect));

							return true;
						}

						transaction.Commit();

						// TO DO: Login
						// Proxy server handler

						peer.SendMessage(new Response(Code, SubCode, new Dictionary<byte, object>()
						{
							{ (byte)MessageParameterCode.SubCodeParameterCode, SubCode },
							{ (byte)MessageParameterCode.PeerIdParameterCode, message.Parameters[(byte)MessageParameterCode.PeerIdParameterCode] }
						}, "", (int)ReturnCode.OK));

						return true;
					}
				}
			}
			catch (Exception ex)
			{
				Log.ErrorFormat("Error login handler: {0}", ex);

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
