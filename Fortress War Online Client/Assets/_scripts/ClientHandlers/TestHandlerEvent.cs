using UnityEngine;

public class TestHandlerEvent : IMessageHandler
{
	public MessageType Type
	{
		get
		{
			return MessageType.Async;
		}
	}

	public byte Code { get { return 1; } }

	public int? SubCode { get { return 2; } }

	public bool HandleMessage(IMessage message)
	{
		Debug.Log("Event from server.");

		return true;
	}
}
