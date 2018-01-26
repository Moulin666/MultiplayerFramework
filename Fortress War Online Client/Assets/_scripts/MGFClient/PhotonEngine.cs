/*
* Created by
* Khusnetdinov Roman
*/

using ExitGames.Client.Photon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class PhotonEngine : MonoBehaviour, IPhotonPeerListener
{
	#region Variables

	/// <summary>
	/// Server Adress. For example - "localhost:5055".
	/// </summary>
	public string ServerAddress = "localhost:5055";
	/// <summary>
	/// ApplicationName on the server.
	/// </summary>
	public string ApplicationName = "TestServer";
	/// <summary>
	/// Use Enctyption?
	/// </summary>
	public bool UseEncryption;

	/// <summary>
	/// SubCodeParameterCode.
	/// </summary>
	[HideInInspector] public byte SubCodeParameterCode;

	/// <summary>
	/// PhotonEngine singleton class.
	/// </summary>
	public static PhotonEngine Instance = null;

	/// <summary>
	/// Client connected state.
	/// </summary>
	public EngineState State { get; protected set; }

	/// <summary>
	/// Client. Photon.PhotonPeer.
	/// </summary>
	public PhotonPeer Peer { get; set; }

	/// <summary>
	/// Client ping. PhotonPeer.RoundTripTime.
	/// </summary>
	public int Ping { get; protected set; }

	/// <summary>
	/// List event handlers.
	/// </summary>
	public List<IMessageHandler> eventHandlerList { get; protected set; }
	/// <summary>
	/// List response handlers
	/// </summary>
	public List<IMessageHandler> responseHandlerList { get; protected set; }

	#endregion

	#region UnityMethods

	private void Awake()
	{
		if (Instance == null)
			Instance = this;
		else if (Instance != this)
			Destroy(gameObject);

		DontDestroyOnLoad(gameObject);
		Initialize();
	}

	private void Start()
	{
		Debug.LogFormat("Connecting to {0} | ApplicationName - {1}", ServerAddress, ApplicationName);
		ConnectToServer(ServerAddress, ApplicationName);
	}

	private void FixedUpdate()
	{
		Ping = Peer.RoundTripTime;

		State.OnUpdate();
	}

	private void OnApplicationQuit()
	{
		Disconnect();
		Instance = null;
	}

	#endregion

	#region My Methods

	/// <summary>
	/// Initialize PhotonEngine.
	/// </summary>
	protected void Initialize()
	{
		State = EngineState.DisconnectedState;
		Application.runInBackground = true;

		GetherMessageHandlers();

		Peer = new PhotonPeer(this, ConnectionProtocol.Udp);
	}

	/// <summary>
	/// Do disconnect client from server.
	/// </summary>
	public void Disconnect()
	{
		if(Peer != null && Peer.PeerState == PeerStateValue.Connected)
		{
			Peer.Disconnect();
		}

		State = EngineState.DisconnectedState;
	}

	/// <summary>
	/// Do connect to server.
	/// </summary>
	/// <param name="serverAddress">Server Address. PhotonEngine.ServerAddress.</param>
	/// <param name="applicationName">Application Name. PhotonEngine.ApplicationName.</param>
	public void ConnectToServer(string serverAddress, string applicationName)
	{
		if (State == EngineState.DisconnectedState)
		{
			Peer.Connect(serverAddress, applicationName);
			State = EngineState.WaitingToConnectState;
		}
	}

	/// <summary>
	/// Get all message handlers and sort it in PhotonEngine.EventHandlerList and PhotonEngine.ResponseHandlerList.
	/// </summary>
	public void GetherMessageHandlers()
	{
		var handlers = from t in Assembly.GetAssembly(GetType()).GetTypes().Where(t => t.GetInterfaces()
					   .Contains(typeof(IMessageHandler))) select Activator.CreateInstance(t) as IMessageHandler;

		Debug.LogFormat("Load handlers. Found {0} handlers.", handlers.Count());

		eventHandlerList = handlers.Where(h => h.Type == MessageType.Async).ToList();
		responseHandlerList = handlers.Where(h => h.Type == MessageType.Response).ToList();
	}

	/// <summary>
	/// Send Request to the server.
	/// </summary>
	/// <param name="request">Photon.OperationRequest</param>
	public void SendRequest(OperationRequest request)
	{
		State.SendRequest(request, true, 0, UseEncryption);
	}

	#endregion

	#region Implements of IPhotonPeerListener

	public void DebugReturn(DebugLevel level, string message)
	{
		Debug.LogFormat("Debug return: ({0}) {1}", level, message);
	}

	public void OnEvent(EventData eventData)
	{
		var message = new Event(eventData.Code, (int)eventData.Parameters[SubCodeParameterCode], eventData.Parameters);
		var handlers = eventHandlerList.Where(h => h.Code == message.Code && h.SubCode == message.SubCode);

		if (handlers == null || handlers.Count() == 0)
		{
			Debug.LogFormat("Default EVENT handler: {0} - subCode: {1}", message.Code, message.SubCode);
		}

		foreach (var handler in handlers)
		{
			handler.HandleMessage(message);
		}
	}

	public void OnOperationResponse(OperationResponse operationResponse)
	{
		var message = new Response(operationResponse.OperationCode, (int)operationResponse.Parameters[SubCodeParameterCode], operationResponse.Parameters, operationResponse.DebugMessage, operationResponse.ReturnCode);
		var handlers = responseHandlerList.Where(h => h.Code == message.Code && h.SubCode == message.SubCode);

		if (handlers == null || handlers.Count() == 0)
		{
			Debug.LogFormat("Default RESPONSE handler: {0} - subCode: {1}", message.Code, message.SubCode);
		}

		foreach (var handler in handlers)
		{
			handler.HandleMessage(message);
		}
	}

	public void OnStatusChanged(StatusCode statusCode)
	{
		switch (statusCode)
		{
			case StatusCode.Connect:
				{
					if (UseEncryption)
					{
						Peer.EstablishEncryption();
						State = EngineState.ConnectingState;
					}
					else
					{
						State = EngineState.ConnectedState;
						Debug.LogFormat("Connected to server.");
					}
				}
				break;
			case StatusCode.EncryptionEstablished:
				{
					State = EngineState.ConnectedState;
					Debug.Log("Encryption established!");
				}
				break;
			case StatusCode.Disconnect:
			case StatusCode.DisconnectByServer:
			case StatusCode.DisconnectByServerUserLimit:
			case StatusCode.DisconnectByServerLogic:
			case StatusCode.EncryptionFailedToEstablish:
			case StatusCode.Exception:
			case StatusCode.ExceptionOnConnect:
			case StatusCode.ExceptionOnReceive:
			case StatusCode.SecurityExceptionOnConnect:
			case StatusCode.TimeoutDisconnect:
				{
					State = EngineState.DisconnectedState;
					Debug.Log("Disconnected from server!");
				}
				break;
			default:
				Debug.Log("Status change to: " + statusCode.ToString());
				break;
		}
	}

	#endregion
}
