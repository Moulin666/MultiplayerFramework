using MultiplayerGameFramework.Interfaces.Client;
using MultiplayerGameFramework.Interfaces.Messaging;
using System;
using System.Collections.Generic;

namespace MultiplayerGameFramework.Implementation.Client
{
	/// <summary>
	/// Sub Server.
	/// </summary>
	public class SubServerClientPeer : IClientPeer
	{
		/// <summary>
		/// Is Proxy?
		/// </summary>
		public bool IsProxy { get; set; }
		/// <summary>
		/// Peer Id.
		/// </summary>
		public Guid PeerId { get; set; }
		/// <summary>
		/// Client data.
		/// </summary>
		/// <typeparam name="T">Class, IClientData.</typeparam>
		/// <returns></returns>
		private readonly Dictionary<Type, IClientData> _clientData;

		/// <summary>
		/// Delegate sub server client peer.
		/// </summary>
		/// <returns></returns>
		public delegate SubServerClientPeer ClientPeerFactory();

		/// <summary>
		/// Base constructor.
		/// </summary>
		/// <param name="clientData">Client data.</param>
		public SubServerClientPeer(IEnumerable<IClientData> clientData)
		{
			_clientData = new Dictionary<Type, IClientData>();

			foreach (var data in clientData)
			{
				_clientData.Add(data.GetType(), data);
			}
		}

		/// <summary>
		/// Disconnect from the server.
		/// </summary>
		public void Disconnect()
		{
		}

		/// <summary>
		/// Send message to the server.
		/// </summary>
		/// <param name="message">Message for sent.</param>
		public void SendMessage(IMessage message)
		{
		}

		/// <summary>
		/// Get client data.
		/// </summary>
		/// <typeparam name="T">class, IClientData.</typeparam>
		/// <returns>T class, IClientData.</returns>
		public T ClientData<T>() where T : class, IClientData
		{
			IClientData result;
			_clientData.TryGetValue(typeof(T), out result);

			if(result != null)
			{
				return result as T;
			}

			return null;
		}
	}
}
