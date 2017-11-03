using MultiplayerGameFramework.Interfaces.Messaging;

namespace MultiplayerGameFramework.Interfaces.Support
{
	public interface IClientCodeRemover
	{
		void RemoveCodes(IMessage message);
	}
}
