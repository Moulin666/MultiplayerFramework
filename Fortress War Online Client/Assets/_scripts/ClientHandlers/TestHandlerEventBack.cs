using UnityEngine;

public class TestHandlerEventBack : IMessageHandler
{
	public MessageType Type
	{
		get
		{
			return MessageType.Async;
		}
	}

	public byte Code { get { return 2; } }

	public int? SubCode { get { return 3; } }

	public bool HandleMessage(IMessage message)
	{
		Debug.Log("Background event from server.");

		return true;
	}
}
