using UnityEngine;

public class TestBackgroundThreadHandler : IMessageHandler
{
	public MessageType Type
	{
		get
		{
			return MessageType.Async;
		}
	}

	public byte Code { get { return 20; } }

	public int? SubCode { get { return 1; } }

	public bool HandleMessage(IMessage message)
	{
		Debug.Log("Background event from server.");

		return true;
	}
}
