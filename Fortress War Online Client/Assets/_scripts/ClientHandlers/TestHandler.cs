using UnityEngine;

public class TestHandler : IMessageHandler
{
	public MessageType Type
	{
		get
		{
			return MessageType.Response;
		}
	}

	public byte Code { get { return 1; } }

	public int? SubCode { get { return 1; } }

	public bool HandleMessage(IMessage message)
	{
		Debug.Log("Response from server.");

		return true;
	}
}
