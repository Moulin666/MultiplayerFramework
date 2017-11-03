using UnityEngine;

public class TestHandler : IMessageHandler
{
	public MessageType Type
	{
		get
		{
			return MessageType.Async;
		}
	}

	public byte Code { get { return 1; } }

	public int? SubCode { get { return 1; } }

	public bool HandleMessage(IMessage message)
	{
        Debug.Log("Loool");

		return true;
	}
}
