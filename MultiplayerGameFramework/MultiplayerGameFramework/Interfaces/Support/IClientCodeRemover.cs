using MultiplayerGameFramework.Interfaces.Messaging;

namespace MultiplayerGameFramework.Interfaces.Support
{
	/// <summary>
	/// Code remover before send to the client.
	/// </summary>
	public interface IClientCodeRemover
	{
		/// <summary>
		/// Remove codes from the message.
		/// </summary>
		/// <param name="message">Message to remove the codes.</param>
		void RemoveCodes(IMessage message);
	}
}
