using ExitGames.Logging;
using MultiplayerGameFramework;
using MultiplayerGameFramework.Implementation.Messaging;
using MultiplayerGameFramework.Interfaces.Client;
using MultiplayerGameFramework.Interfaces.Messaging;
using Photon.SocketServer;
using PhotonHostRuntimeInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MGF_Photon.Implementation.Client
{
	public class PhotonClientPeer : ClientPeer, IClientPeer
	{
		/// <summary>
		/// Is Proxy?
		/// </summary>
		public bool IsProxy { get { return false; } set {  } }
		/// <summary>
		/// Peer Id.
		/// </summary>
		public Guid PeerId { get; set; }
		public IConnectionCollection<IClientPeer> ConnectionCollection { get; set; }

		protected ILogger Log;

		private readonly IServerApplication _server;
		private readonly IHandlerList<IClientPeer> _handlerList;
		private readonly Dictionary<Type, IClientData> _clientData;

		#region FactoryMethods

		public delegate PhotonClientPeer ClientPeerFactory(InitRequest initRequest);

		#endregion

		/// <summary>
		/// Base constructor.
		/// </summary>
		/// <param name="initRequest"></param>
		/// <param name="log"></param>
		/// <param name="server"></param>
		/// <param name="clientData"></param>
		/// <param name="connectionCollection"></param>
		/// <param name="handlerList"></param>
		public PhotonClientPeer(InitRequest initRequest,
			ILogger log,
			IServerApplication server,
			IEnumerable<IClientData> clientData,
			IConnectionCollection<IClientPeer> connectionCollection,
			IHandlerList<IClientPeer> handlerList)
			: base(initRequest)
		{
			Log = log;
			_server = server;
			_handlerList = handlerList;

			Log.DebugFormat("Created client peer");

			ConnectionCollection = connectionCollection;
			ConnectionCollection.Connect(this);
			PeerId = Guid.NewGuid();
			_clientData = new Dictionary<Type, IClientData>();

			foreach(var data in clientData)
			{
				_clientData.Add(data.GetType(), data);
			}
		}

		protected override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters)
		{
			Log.DebugFormat("Handeling operation request");

			_handlerList.HandleMessage(new Request(operationRequest.OperationCode,
				operationRequest.Parameters.ContainsKey(_server.SubCodeParameterCode) ? (int?)Convert.ToInt32(operationRequest.Parameters[_server.SubCodeParameterCode])
				: null, operationRequest.Parameters), this);
		}

		protected override void OnDisconnect(DisconnectReason reasonCode, string reasonDetail)
		{
			ConnectionCollection.Disconnect(this);

			Log.DebugFormat("Client Disconnected {0}", PeerId);
		}

		/// <summary>
		/// Send message to the client.
		/// </summary>
		/// <param name="message">Message for sent.</param>
		public void SendMessage(IMessage message)
		{
			if (!message.Parameters.Keys.Contains(_server.SubCodeParameterCode))
			{
				message.Parameters.Add(_server.SubCodeParameterCode, message.SubCode);
			}

			if (message is Event)
			{
				SendEvent(new EventData(message.Code) { Parameters = message.Parameters }, new SendParameters());
			}

			var response = message as Response;

			if(response != null)
			{
				SendOperationResponse(new OperationResponse(response.Code, response.Parameters) { DebugMessage = response.DebugMessage, ReturnCode = response.ReturnCode }, new SendParameters());
			}
		}

		/// <summary>
		/// Client data.
		/// </summary>
		/// <typeparam name="T">Class, IClientData.</typeparam>
		/// <returns></returns>
		public T ClientData<T>() where T : class, IClientData
		{
			IClientData result;
			_clientData.TryGetValue(typeof(T), out result);

			if (result != null)
			{
				return result as T;
			}

			return null;
		}
	}
}
