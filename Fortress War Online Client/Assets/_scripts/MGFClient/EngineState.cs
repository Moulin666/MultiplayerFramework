using ExitGames.Client.Photon;
using System;

public class EngineState
{
	/// <summary>
	/// Client disconnected from the server.
	/// </summary>
	public static EngineState DisconnectedState = new EngineState() { OnUpdate = () => { }, SendRequest = (r, s, c, e) => { } };
	/// <summary>
	/// Client waiting connect established to the server.
	/// </summary>
	public static EngineState WaitingToConnectState = new EngineState() { OnUpdate = DoUpdate, SendRequest = DoSend };
	/// <summary>
	/// Client connecting to the server.
	/// </summary>
	public static EngineState ConnectingState = new EngineState() { OnUpdate = DoUpdate, SendRequest = DoSend };
	/// <summary>
	/// Client connected to the server.
	/// </summary>
	public static EngineState ConnectedState = new EngineState() { OnUpdate = DoUpdate, SendRequest = DoSend };

	/// <summary>
	/// Update action.
	/// </summary>
	public Action OnUpdate { get; set; }
	/// <summary>
	/// Send request action.
	/// </summary>
	public Action<OperationRequest, bool, byte, bool> SendRequest { get; set; }

	protected EngineState() { }

	/// <summary>
	/// Base update to call Peer.Service();
	/// </summary>
	protected static void DoUpdate()
	{
		PhotonEngine.Instance.Peer.Service();
	}

	/// <summary>
	/// Send request to the server.
	/// </summary>
	/// <param name="request">Photon.OperationRequest.</param>
	/// <param name="sendReliable">Reliable?</param>
	/// <param name="channelId">ChannelId.</param>
	/// <param name="encrypt">Encrypt?</param>
	protected static void DoSend(OperationRequest request, bool sendReliable, byte channelId, bool encrypt)
	{
		PhotonEngine.Instance.Peer.OpCustom(request, sendReliable, channelId, encrypt);
	}
}
