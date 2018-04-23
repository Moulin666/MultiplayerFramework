using GameCommon;

public class WorldEnterHandler : IMessageHandler
{
	public MessageType Type => MessageType.Response;

	public byte Code => (byte)MessageOperationCode.GameOperationCode;

	public int? SubCode => (int)MessageSubCode.WorldEnterSubCode;

	public bool HandleMessage(IMessage message)
	{
		// Instantiate all other users

		return true;
	}
}
