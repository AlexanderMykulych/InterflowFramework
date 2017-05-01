using EasyNetQ;
using EasyNetQ.Rx;
using InterflowFramework.Core.Channel.Transport.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reactive.Subjects;
using Newtonsoft.Json;
using InterflowFramework.Core.Channel.Transport.Const;

namespace InterflowFramework.RabbitMq.Transport
{
	public class RabbitMqTransport : BaseTransport
	{
		public string Host;
		public string TopicId;
		private IBus _bus;
		private bool PushEnable;
		private bool SubscribeEnable;
		public IConnectableObservable<RabbitMessage> Topic;
		private IDisposable TopicDisposer;

		public IBus Bus {
			get {
				if(_bus == null && !string.IsNullOrEmpty(Host)) {
					_bus = RabbitHutch.CreateBus(Host);
				}
				return _bus;
			}
		}
		
		public RabbitMqTransport(string host, string topic, bool pushToRabbitEnable = true, bool subscribeRabbitEnable = true) {
			Host = host;
			TopicId = topic;
			PushEnable = pushToRabbitEnable;
			SubscribeEnable = subscribeRabbitEnable;
		}
		public override void Push(object message)
		{
			if(!PushEnable) {
				return;
			}
			Bus.Publish<RabbitMessage>(SerializeMessage(message), TopicId);
		}

		protected virtual RabbitMessage SerializeMessage(object message)
		{
			return new RabbitMessage() {
				Data = JsonConvert.SerializeObject(message),
				Type = message.GetType().AssemblyQualifiedName
			};
		}

		public override void Enable()
		{
			base.Enable();
			SubscribeRabbit();
		}

		protected virtual void SubscribeRabbit() {
			if(!SubscribeEnable) {
				return;
			}
			if (Topic == null)
			{
				Topic = Bus.ToObservable<RabbitMessage>(TopicId);
				Topic.Subscribe(PushNext);
			}
			TopicDisposer = Topic.Connect();
		}
		public override void Dispose()
		{
			base.Dispose();
			if (TopicDisposer != null)
			{
				TopicDisposer.Dispose();
			}
		}
		public virtual void PushNext(object message) {
			var deserializedMessage = DeserializeMessage(message);
			if(deserializedMessage != null) {
				Subscriber.Execute(TransportEvent.onMessage, deserializedMessage);
			}
		}
		protected virtual object DeserializeMessage(object message) {
			if(message is RabbitMessage) {
				var rabbitMessage = (RabbitMessage)message;
				var res = JsonConvert.DeserializeObject(rabbitMessage.Data, Type.GetType(rabbitMessage.Type));
				return res;
			}
			return null;
		}
	}
}
