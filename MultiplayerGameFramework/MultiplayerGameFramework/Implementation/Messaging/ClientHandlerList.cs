using MultiplayerGameFramework.Interfaces.Client;
using MultiplayerGameFramework.Interfaces.Messaging;
using System.Collections.Generic;
using System.Linq;

namespace MultiplayerGameFramework.Implementation.Messaging
{
	public class ClientHandlerList : IHandlerList<IClientPeer>
	{
		private readonly List<IHandler<IClientPeer>> _requestSubCodeHandlerList;
		private readonly List<IHandler<IClientPeer>> _requestCodeHandlerList;

		public ClientHandlerList(IEnumerable<IHandler<IClientPeer>> handlers)
		{
			_requestCodeHandlerList = new List<IHandler<IClientPeer>>();
			_requestSubCodeHandlerList = new List<IHandler<IClientPeer>>();

			foreach (var handler in handlers)
			{
				RegisterHandler(handler);
			}
		}

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
