using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterflowFramework.Core.Channel.Subscriber.Interface
{
	public interface ISubscriber
	{
		ISubscriber On(string key, Action<object> onPublish);
		ISubscriber Unsubscribe(string key);
		void Unsubscribe();
	}
}
