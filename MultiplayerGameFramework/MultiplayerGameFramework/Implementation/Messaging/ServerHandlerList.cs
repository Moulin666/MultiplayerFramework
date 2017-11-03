using MultiplayerGameFramework.Interfaces.Messaging;
using MultiplayerGameFramework.Interfaces.Server;
using System.Collections.Generic;
using System.Linq;

namespace MultiplayerGameFramework.Implementation.Messaging
{
	public class ServerHandlerList : IHandlerList<IServerPeer>
	{
		private readonly IDefaultRequestHandler<IServerPeer> _defaultRequestHandler;
		private readonly IDefaultResponseHandler<IServerPeer> _defaultResponseHandler;
		private readonly IDefaultEventHandler<IServerPeer> _defaultEventHandler;

		private readonly List<IHandler<IServerPeer>> _requestSubCodeHandlerList;
		private readonly List<IHandler<IServerPeer>> _requestCodeHandlerList;

		private readonly List<IHandler<IServerPeer>> _responseSubCodeHandlerList;
		private readonly List<IHandler<IServerPeer>> _responseCodeHandlerList;

		private readonly List<IHandler<IServerPeer>> _eventSubCodeHandlerList;
		private readonly List<IHandler<IServerPeer>> _eventCodeHandlerList;

		public ServerHandlerList(IEnumerable<IHandler<IServerPeer>> handlers,
			IDefaultRequestHandler<IServerPeer> defaultRequestHandler,
			IDefaultResponseHandler<IServerPeer> defaultResponseHandler,
			IDefaultEventHandler<IServerPeer> defaultEventHandler)
		{
			_defaultEventHandler = defaultEventHandler;
			_defaultRequestHandler = defaultRequestHandler;
			_defaultResponseHandler = defaultResponseHandler;

			_requestCodeHandlerList = new List<IHandler<IServerPeer>>();
			_requestSubCodeHandlerList = new List<IHandler<IServerPeer>>();

			_responseCodeHandlerList = new List<IHandler<IServerPeer>>();
			_responseSubCodeHandlerList = new List<IHandler<IServerPeer>>();

			_eventCodeHandlerList = new List<IHandler<IServerPeer>>();
			_eventSubCodeHandlerList = new List<IHandler<IServerPeer>>();

			foreach(var handler in handlers)
			{
				RegisterHandler(handler);
			}
		}

		public bool RegisterHandler(IHandler<IServerPeer> handler)
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

			if ((handler.Type & MessageType.Response) == MessageType.Response)
			{
				if (handler.SubCode.HasValue)
				{
					_responseSubCodeHandlerList.Add(handler);
					registered = true;
				}
				else
				{
					_responseCodeHandlerList.Add(handler);
					registered = true;
				}
			}

			if ((handler.Type & MessageType.Async) == MessageType.Async)
			{
				if (handler.SubCode.HasValue)
				{
					_eventSubCodeHandlerList.Add(handler);
					registered = true;
				}
				else
				{
					_eventCodeHandlerList.Add(handler);
					registered = true;
				}
			}

			return registered;
		}

		public bool HandleMessage(IMessage message, IServerPeer peer)
		{
			var handled = false;

			IEnumerable<IHandler<IServerPeer>> handlers;

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
						_defaultRequestHandler.HandleMessage(message, peer);
					}
					
					foreach(var handler in handlers)
					{
						handler.HandleMessage(message, peer);
						handled = true;
					}
					break;

				case MessageType.Response:
					handlers = _responseSubCodeHandlerList.Where(h => h.Code == message.Code && h.SubCode == message.SubCode);

					if (handlers == null || handlers.Count() == 0)
					{
						handlers = _responseCodeHandlerList.Where(h => h.Code == message.Code);
					}

					// still no message
					if (handlers == null || handlers.Count() == 0)
					{
						_defaultResponseHandler.HandleMessage(message, peer);
					}

					foreach (var handler in handlers)
					{
						handler.HandleMessage(message, peer);
						handled = true;
					}
					break;

				case MessageType.Async:
					handlers = _eventSubCodeHandlerList.Where(h => h.Code == message.Code && h.SubCode == message.SubCode);

					if (handlers == null || handlers.Count() == 0)
					{
						handlers = _eventCodeHandlerList.Where(h => h.Code == message.Code);
					}

					// still no message
					if (handlers == null || handlers.Count() == 0)
					{
						_defaultEventHandler.HandleMessage(message, peer);
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
