using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterflowFramework.Core.Channel.Subscriber.Interface;
using InterflowFramework.Core.Channel.Subscriber.Model;

namespace InterflowFramework.Core.Channel.OutputPoint.Model
{
	public abstract class BaseOutputPoint : IOutputPoint
	{
		private IExecutorSubscriber _subscriber;
		protected virtual IExecutorSubscriber Subscriber {
			get {
				if (_subscriber == null)
				{
					_subscriber = new BaseSubscriber();
				}
				return _subscriber;
			}
		}
		public virtual void Disable() {
			Subscriber.Disable();
		}

		public virtual void Dispose() {
			Subscriber.Dispose();
		}

		public virtual void Enable() {
			Subscriber.Enable();
		}

		public virtual ISubscriber On(string key, Action<object> onPublish)
		{
			Subscriber.On(key, onPublish);
			return this;
		}

		public virtual ISubscriber Unsubscribe(string key)
		{
			Subscriber.Unsubscribe(key);
			return this;
		}

		public virtual void Unsubscribe()
		{
			Subscriber.Unsubscribe();
		}

		public abstract void Push(object message);
	}
}
