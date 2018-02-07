using ExitGames.Logging;
using MGF_Photon.Implementation.Codes;
using MGF_Photon.Implementation.Data;
using MGF_Photon.Implementation.Operation;
using MGF_Photon.Implementation.Operation.Data;
using MGF_Photon.Implementation.Server;
using MultiplayerGameFramework.Implementation.Config;
using MultiplayerGameFramework.Implementation.Messaging;
using MultiplayerGameFramework.Interfaces.Config;
using MultiplayerGameFramework.Interfaces.Messaging;
using MultiplayerGameFramework.Interfaces.Server;
using Photon.SocketServer;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace MGF_Photon.Implementation.Handler
{
	public class HandleServerRegistration : ServerHandler
	{
		private readonly IServerType _serverType;

		public ILogger Log { get; set; }

		private ServerConfiguration _serverConfiguration;

		public HandleServerRegistration(ILogger log, IServerType serverType, ServerConfiguration serverConfiguration)
		{
			Log = log;
			_serverType = serverType;
			_serverConfiguration = serverConfiguration;
		}

		public override MessageType Type
		{
			get
			{
				return MessageType.Request;
			}
		}

		public override byte Code
		{
			get
			{
				return 0;
			}
		}

		public override int? SubCode
		{
			get
			{
				return null;
			}
		}

		public override bool OnHandleMessage(IMessage message, IServerPeer serverPeer)
		{
			var peer = serverPeer as PhotonServerPeer;

			if(peer != null)
			{
				return OnHandleMessage(message, peer);
			}

			return false;
		}

		protected bool OnHandleMessage(IMessage message, PhotonServerPeer serverPeer)
		{
			OperationResponse operationResponse;

			if (serverPeer.Registered)
			{
				operationResponse = new OperationResponse(message.Code)
				{
					ReturnCode = (short)ErrorCode.InternalServerError,
					DebugMessage = "Already registered"
				};
			}
			else
			{
				var registerRequest = new RegisterSubServer(serverPeer.protocol, message);

				if(!registerRequest.IsValid)
				{
					string msg = registerRequest.GetErrorMessage();

					if(Log.IsDebugEnabled)
						Log.DebugFormat("Invalid register request {0}", msg);

					operationResponse = new OperationResponse(message.Code)
						{ DebugMessage = msg, ReturnCode = (short)ErrorCode.OperationInvalid };
				}
				else
				{
					XmlSerializer mySerializer = new XmlSerializer(typeof(RegisterSubServerData));
					StringReader inStream = new StringReader(registerRequest.RegisterSubServerOperation);
					var registerData = (RegisterSubServerData)mySerializer.Deserialize(inStream);

					if (Log.IsDebugEnabled)
						Log.DebugFormat("Received register request: Address={0}, UdpPort={1}, TcpPort={2}, Type={3}", 
							registerData.GameServerAddress, registerData.UdpPort, registerData.TcpPort, registerData.ServerType);

					var serverData = serverPeer.ServerData<ServerData>();

					if(serverData == null)
						Log.DebugFormat("ServerData is NULL");	

					if (registerData.UdpPort.HasValue)
						serverData.UdpAddress = registerData.GameServerAddress + ":" + registerData.UdpPort;

					if (registerData.TcpPort.HasValue)
					{
						serverData.TcpAddress = registerData.GameServerAddress + ":" + registerData.TcpPort;
					}

					serverData.ServerId = registerData.ServerId;

					serverData.ServerType = registerData.ServerType;

					serverPeer.ServerType = _serverType.GetServerType(registerData.ServerType);

					serverData.ApplicationName = registerData.ServerName;

					Log.DebugFormat("ServerData: id={0} AppName={1} Type={2} | TCP: {3} UDP: {4}",
						serverData.ServerId, serverData.ApplicationName, serverData.ServerType,
						serverData.TcpAddress, serverData.UdpAddress);

					operationResponse = new OperationResponse(message.Code, new Dictionary<byte, object>()
						{ { _serverConfiguration.SubCodeParameterCode, 0 } });

					serverPeer.Registered = true;
				}
			}

			serverPeer.SendOperationResponse(operationResponse, new SendParameters());

			return true;
		}
	}
}
