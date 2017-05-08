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
using InterflowFramework.Core.Channel.Identified;

namespace InterflowFramework.RabbitMq.Transport
{
	public class RabbitMqTransport : BaseTransport
	{
		public string ConnectionString;
		public string TopicId;
		private IBus _bus;
		private bool PushEnable;
		private bool SubscribeEnable;
		public IConnectableObservable<RabbitMessage> Topic;
		private IDisposable TopicDisposer;
		private bool CatchResponse;

		public IBus Bus {
			get {
				if(_bus == null && !string.IsNullOrEmpty(ConnectionString)) {
					_bus = RabbitHutch.CreateBus(ConnectionString);
				}
				return _bus;
			}
		}

		public RabbitMqTransport(string connectionString, string topic, bool pushToRabbitEnable = true, bool subscribeRabbitEnable = true, bool catchResponse = false)
		{
			ConnectionString = connectionString;
			TopicId = topic;
			PushEnable = pushToRabbitEnable;
			SubscribeEnable = subscribeRabbitEnable;
			CatchResponse = catchResponse;
		}
		public override void Push(object message)
		{
			if(!PushEnable) {
				return;
			}
			var rabbitMessage = SerializeMessage(message);
			if (CatchResponse && message is IIdentified)
			{
				SubscribeQueueForResponse((IIdentified)message);
			}
			Bus.Send(TopicId, rabbitMessage);
		}

		private void SubscribeQueueForResponse(IIdentified id)
		{
			Bus.Receive<RabbitMessage>(id.GetId(), message => onResponse(message));
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
			Bus.Receive<RabbitMessage>(TopicId, message => PushNext(message));
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
			if (message is RabbitMessage)
			{
				var rabbitMessage = (RabbitMessage)message;
				var deserializedMessage = DeserializeMessage(rabbitMessage.Data, rabbitMessage.Type);
				if (rabbitMessage.Mode == TRabbitMessageMode.Message) {
					Subscriber.Execute(TransportEvent.onMessage, deserializedMessage);
				} else if(rabbitMessage.Mode == TRabbitMessageMode.Response) {
					Subscriber.Execute(TransportEvent.onResponse, deserializedMessage);
				}
			}
			
		}
		protected virtual object DeserializeMessage(string message, string type) {
			var res = JsonConvert.DeserializeObject(message, Type.GetType(type));
			return res;
		}

		public override void Response(object message)
		{
			if (!(message is IIdentified))
			{
				return;
			}
			var rabbitMessage = SerializeMessage(message);
			rabbitMessage.Mode = TRabbitMessageMode.Response;
			Bus.Send(((IIdentified)message).GetId(), rabbitMessage);
		}
		private void onResponse(RabbitMessage obj)
		{
			Subscriber.Execute(TransportEvent.onResponse, DeserializeMessage(obj.Data, obj.Type));
		}
	}
}
