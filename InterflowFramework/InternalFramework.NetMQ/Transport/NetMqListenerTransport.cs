using InterflowFramework.Core.Channel.Transport.Const;
using InterflowFramework.Core.Channel.Transport.Model;
using InterflowFramework.Core.Message.Model.Text;
using NetMQ;
using NetMQ.ReactiveExtensions;
using NetMQ.Sockets;
using ProtoBuf.Meta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace InternalFramework.NetMQ.Transport
{
	public class NetMqListenerTransport<T> : BaseTransport
	{
		public string ConnectionString;
		public SubscriberNetMq<T> Listener;

		public NetMqListenerTransport(string connectionString) {
			ConnectionString = connectionString;
		}
		public override void Push(object message)
		{
			Subscriber.Execute(TransportEvent.onMessage, message);
		}
		public override void Enable()
		{
			base.Enable();
			CreateSocketConnection();
		}
		public override void Disable()
		{
			base.Disable();
			DisconnectSocketListener();
		}
		public override void Dispose()
		{
			base.Dispose();
			Listener.Dispose();
		}
		protected virtual void CreateSocketConnection() {
			Listener = new SubscriberNetMq<T>(ConnectionString);
			Listener.Subscribe(x => Push(x));
			Subscriber.Execute(TransportEvent.onConnect);
		}
		protected virtual void DisconnectSocketListener() {
			Subscriber.Execute(TransportEvent.onDisconnect);
		}

		public override void Response(object message)
		{
			throw new NotImplementedException();
		}
	}
}
