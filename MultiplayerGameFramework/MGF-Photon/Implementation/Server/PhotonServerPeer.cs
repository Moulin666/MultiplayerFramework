using ExitGames.Logging;
using MultiplayerGameFramework;
using MultiplayerGameFramework.Implementation.Messaging;
using MultiplayerGameFramework.Interfaces.Config;
using MultiplayerGameFramework.Interfaces.Messaging;
using MultiplayerGameFramework.Interfaces.Server;
using Photon.SocketServer;
using Photon.SocketServer.ServerToServer;
using PhotonHostRuntimeInterfaces;
using System;
using System.Collections.Generic;

namespace MGF_Photon.Implementation.Server
{
	public class PhotonServerPeer : IServerPeer
	{
		private readonly Dictionary<Type, IServerData> _serverData = new Dictionary<Type, IServerData>();

		/// <summary>
		/// Server now registered?
		/// </summary>
		public bool Registered { get; set; }
		public ILogger Log;

		private ServerApplication _server;

		/// <summary>
		/// Server application.
		/// </summary>
		public IServerApplication Server
		{
			get { return _server; }
			set { _server = value as ServerApplication; }
		}

		private S2SPeerBase _peer;

		public IRpcProtocol protocol
		{
			get { return _peer.Protocol; }
		}

		/// <summary>
		/// Type of server.
		/// </summary>
		public IServerType ServerType { get; set; }

		private readonly IServerConnectionCollection<IServerType, IServerPeer> _serverCollection;
		private readonly IHandlerList<IServerPeer> _handlerList;

		#region FactoryMethods

		public delegate PhotonServerPeer ServerPeerFactory(S2SPeerBase peer, bool isPeer);

		#endregion

		public PhotonServerPeer(S2SPeerBase peer,
			bool isPeer,
			ILogger log,
			IServerApplication server,
			IEnumerable<IServerData> serverData,
			IServerConnectionCollection<IServerType, IServerPeer> serverCollection,
			IHandlerList<IServerPeer> handlerList)
		{
			_peer = peer;
			Log = log;
			Server = server;

			Log.DebugFormat("Created server peer");

			_serverCollection = serverCollection;
			_handlerList = handlerList;
			_serverCollection.Connect(this);

			foreach (var data in serverData)
			{
				Log.DebugFormat("data {0}-{1}", data.ToString(), data.GetType().ToString());
				_serverData.Add(data.GetType(), data);
			}
		}

		public void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters)
		{
			HandleMessage(new Request(operationRequest.OperationCode,
				operationRequest.Parameters.ContainsKey(Server.SubCodeParameterCode) ? (int?)Convert.ToInt32(operationRequest.Parameters[Server.SubCodeParameterCode])
				: null, operationRequest.Parameters));
		}

		public void OnEvent(IEventData eventData, SendParameters sendParameters)
		{
			HandleMessage(new Event(eventData.Code,
				eventData.Parameters.ContainsKey(Server.SubCodeParameterCode) ? (int?)Convert.ToInt32(eventData.Parameters[Server.SubCodeParameterCode])
				: null, eventData.Parameters));
		}

		public void OnOperationResponse(OperationResponse operationResponse, SendParameters sendParameters)
		{
			HandleMessage(new Response(operationResponse.OperationCode,
				operationResponse.Parameters.ContainsKey(Server.SubCodeParameterCode) ?
				(int?)Convert.ToInt32(operationResponse.Parameters[Server.SubCodeParameterCode]) : null,
				operationResponse.Parameters,
				operationResponse.DebugMessage,
				operationResponse.ReturnCode));
		}

		public void OnDisconnect (DisconnectReason reasonCode, string reasonDetail)
		{
			_serverCollection.Disconnect(this);
			Log.DebugFormat("Server disconnected {0} - {1}", reasonCode.ToString(), reasonDetail);
		}

		public void Register()
		{
			_server.Register(this);
		}

		/// <summary>
		/// Server date.
		/// </summary>
		/// <typeparam name="T">class, IServerData.</typeparam>
		/// <returns>T class, IServer data.</returns>
		public T ServerData<T>() where T : class, IServerData
		{
			IServerData result;
			_serverData.TryGetValue(typeof(T), out result);

			if(result != null)
			{
				return result as T;
			}

			return null;
		}

		/// <summary>
		/// Handle message from the server.
		/// </summary>
		/// <param name="message"></param>
		public void HandleMessage(IMessage message)
		{
			Log.Debug("Handle message.");

			_handlerList.HandleMessage(message, this);
		}

		/// <summary>
		/// Send message to the server.
		/// </summary>
		/// <param name="message"></param>
		public void SendMessage(IMessage message)
		{
			var response = message as Response;

			if (response != null)
			{
				_peer.SendOperationResponse(new OperationResponse(message.Code, message.Parameters) { DebugMessage = response.DebugMessage, ReturnCode = response.ReturnCode }, new SendParameters());
			}
			else if (message is Event)
			{
				_peer.SendEvent(new EventData(message.Code, message.Parameters), new SendParameters());
			}
			else if (message is Request)
			{
				_peer.SendOperationRequest(new OperationRequest(message.Code, message.Parameters), new SendParameters());
			}
		}

		public void SendOperationRequest(OperationRequest request, SendParameters sendParameters)
		{
			_peer.SendOperationRequest(request, sendParameters);
		}

		public void SendOperationResponse(OperationResponse response, SendParameters sendParameters)
		{
			_peer.SendOperationResponse(response, sendParameters);
		}

		public void SendEvent(EventData eventData, SendParameters sendParameters)
		{
			_peer.SendEvent(eventData, sendParameters);
		}

		/// <summary>
		/// Disconnect from the server.
		/// </summary>
		public void Disconnect()
		{
			_peer.Disconnect();
		}
	}
}
