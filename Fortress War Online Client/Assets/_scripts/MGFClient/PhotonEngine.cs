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

	public string ServerAdress = "localhost:5055";
	public string ApplicationName = "TestServer";
	public bool UseEncryption;

	[HideInInspector]
	public byte SubCodeParameterCode;

	public static PhotonEngine instance = null;

	public EngineState State { get; protected set; }

	public PhotonPeer Peer { get; set; }

	public int Ping { get; protected set; }

	public List<IMessageHandler> eventHandlerList { get; protected set; }
	public List<IMessageHandler> responseHandlerList { get; protected set; }

	#endregion

	#region UnityMethods

	private void Awake()
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);

		DontDestroyOnLoad(gameObject);
		Initialize();
	}

	private void Start()
	{
		Debug.LogFormat("Connecting to {0} | ApplicationName - {1}", ServerAdress, ApplicationName);
		ConnectToServer(ServerAdress, ApplicationName);
	}

	private void FixedUpdate()
	{
		Ping = Peer.RoundTripTime;

		State.OnUpdate();
	}

	private void OnApplicationQuit()
	{
		Disconnect();
		instance = null;
	}

	#endregion

	#region My Methods

	protected void Initialize()
	{
		State = EngineState.DisconnectedState;
		Application.runInBackground = true;

		GetherMessageHandlers();

		Peer = new PhotonPeer(this, ConnectionProtocol.Udp);
	}

	public void Disconnect()
	{
		if(Peer != null && Peer.PeerState == PeerStateValue.Connected)
		{
			Peer.Disconnect();
		}

		State = EngineState.DisconnectedState;
	}

	public void ConnectToServer(string serverAddress, string applicationName)
	{
		if (State == EngineState.DisconnectedState)
		{
			Peer.Connect(serverAddress, applicationName);
			State = EngineState.WaitingToConnectState;
		}
	}

	public void GetherMessageHandlers()
	{
		var handlers = from t in Assembly.GetAssembly(GetType()).GetTypes().Where(t => t.GetInterfaces().Contains(typeof(IMessageHandler))) select Activator.CreateInstance(t) as IMessageHandler;

		Debug.LogFormat("Load handlers. Found {0} handlers.", handlers.Count());

		eventHandlerList = handlers.Where(h => h.Type == MessageType.Async).ToList();
		responseHandlerList = handlers.Where(h => h.Type == MessageType.Response).ToList();
	}

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
