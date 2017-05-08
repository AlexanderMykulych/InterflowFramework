using InterflowFramework.Core.Channel.Subscriber.Const;
using InterflowFramework.Core.Channel.Subscriber.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterflowFramework.Core.Channel.Subscriber.Model
{
	public class BaseSubscriber : IExecutorSubscriber
	{
		Dictionary<string, List<Action<object>>> Publisher = new Dictionary<string, List<Action<object>>>();
		Dictionary<string, List<Action<object>>> OncePublisher = new Dictionary<string, List<Action<object>>>();
		bool _isEnabled = false;
		public ISubscriber On(string key, Action<object> onPublish)
		{
			if (!Publisher.ContainsKey(key))
			{
				Publisher[key] = new List<Action<object>>();
			}
			Publisher[key].Add(onPublish);
			return this;
		}

		public ISubscriber Unsubscribe(string key)
		{
			if (Publisher.ContainsKey(key)) {
				Publisher.Remove(key);
			}
			return this;
		}

		public void Unsubscribe()
		{
			Publisher.Clear();
		}

		public virtual void Execute(string key, object message) {
			if(!_isEnabled) {
				return;
			}
			ExecuteOnce(key, message);
			if (Publisher.ContainsKey(key)) {
				Publisher[key].ForEach(x =>
				{
					try {
						x.Invoke(message);
					} catch(Exception e) {
						ExecuteNoSafe(SubscriberEvent.onPublishError, e);
					}
				});
			}
		}
		protected virtual void ExecuteNoSafe(string key, object message) {
			if (!_isEnabled)
			{
				return;
			}
			if (Publisher.ContainsKey(key))
			{
				Publisher[key].ForEach(x => x.Invoke(message));
			}
		}

		public void Enable()
		{
			_isEnabled = true;
		}

		public void Disable()
		{
			_isEnabled = false;
		}

		public void Dispose()
		{
			Unsubscribe();
			Publisher = null;
		}

		public ISubscriber Once(string key, Action<object> onPublish)
		{
			if (!OncePublisher.ContainsKey(key))
			{
				OncePublisher[key] = new List<Action<object>>();
			}
			OncePublisher[key].Add(onPublish);
			return this;
		}
		public virtual void ExecuteOnce(string key, object message)
		{
			if (!_isEnabled)
			{
				return;
			}
			if (OncePublisher.ContainsKey(key))
			{
				OncePublisher[key].ForEach(x =>
				{
					try
					{
						x.Invoke(message);
					}
					catch (Exception e)
					{
						ExecuteNoSafe(SubscriberEvent.onPublishError, e);
					}
				});
				OncePublisher.Remove(key);
			}
		}
	}
}
