using MultiplayerGameFramework.Interfaces.Client;
using MultiplayerGameFramework.Interfaces.Messaging;
using System.Collections.Generic;
using System.Linq;

namespace MultiplayerGameFramework.Implementation.Messaging
{
	/// <summary>
	/// List of handlers.
	/// </summary>
	public class ClientHandlerList : IHandlerList<IClientPeer>
	{
		private readonly List<IHandler<IClientPeer>> _requestSubCodeHandlerList;
		private readonly List<IHandler<IClientPeer>> _requestCodeHandlerList;

		/// <summary>
		/// Base constructor.
		/// </summary>
		/// <param name="handlers">Handlers for store.</param>
		public ClientHandlerList(IEnumerable<IHandler<IClientPeer>> handlers)
		{
			_requestCodeHandlerList = new List<IHandler<IClientPeer>>();
			_requestSubCodeHandlerList = new List<IHandler<IClientPeer>>();

			foreach (var handler in handlers)
			{
				RegisterHandler(handler);
			}
		}

		/// <summary>
		/// Register new handler.
		/// </summary>
		/// <param name="handler">IHandler for register.</param>
		/// <returns>Register state.</returns>
		public bool RegisterHandler(IHandler<IClientPeer> handler)
		{
			var registered = false;

			if ((handler.Type & MessageType.Request) == MessageType.Request)
			{
				if (handler.SubCode.HasValue)
				{
					_requestSubCodeHandlerList.Add(handler);
					registered = true;
				}
				else
				{
					_requestCodeHandlerList.Add(handler);
					registered = true;
				}
			}

			return registered;
		}

		/// <summary>
		/// Handle message.
		/// </summary>
		/// <param name="message">Message from client.</param>
		/// <param name="peer">Client.</param>
		/// <returns>Handle state.</returns>
		public bool HandleMessage(IMessage message, IClientPeer peer)
		{
			var handled = false;

			IEnumerable<IHandler<IClientPeer>> handlers;
			
			switch (message.Type)
			{
				case MessageType.Request:
					handlers = _requestSubCodeHandlerList.Where(h => h.Code == message.Code && h.SubCode == message.SubCode);

					if (handlers == null || handlers.Count() == 0)
					{
						handlers = _requestCodeHandlerList.Where(h => h.Code == message.Code);
					}

					// still no message
					if (handlers == null || handlers.Count() == 0)
					{
						// no default for incoming client request
					}

					foreach (var handler in handlers)
					{
						handler.HandleMessage(message, peer);
						handled = true;
					}
					break;
			}

			return handled;
		}
	}
}
