using InterflowFramework.Core.Channel.Transport.Const;
using InterflowFramework.Core.Channel.Transport.Model;
using InterflowFramework.Core.Message;
using InterflowFramework.Core.Message.Model.Text;
using NetMQ.ReactiveExtensions;
using NetMQ.Sockets;
using ProtoBuf.Meta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternalFramework.NetMQ.Transport
{
	public class NetMqPublisherTransport<T> : BaseTransport
	{
		public string ConnectionString;
		public NetMqPublisherTransport(string connectionString) {
			ConnectionString = connectionString;
		}
		public IPublisherNetMq<T> Publisher;
		public override void Push(object message)
		{
			try
			{
				if(message is T) {
					var textMsg = (T)message;
					Publisher.OnNext(textMsg);
				}
				
			}
			catch (Exception)
			{

			}
			Subscriber.Execute(TransportEvent.onMessage, message);
		}
		public override void Enable()
		{
			base.Enable();
			EnablePublisher();
		}

		protected virtual void EnablePublisher()
		{
			Publisher = new PublisherNetMq<T>(ConnectionString);
		}

		public override void Disable()
		{
			base.Disable();
		}
		public override void Dispose()
		{
			base.Dispose();
			Publisher.Dispose();
		}
	}
}
