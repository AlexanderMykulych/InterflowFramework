using InterflowFramework.Core.Channel.Enabler.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterflowFramework.Core.Channel.Subscriber.Interface
{
	public interface IExecutorSubscriber: ISubscriber, IEnabler, IDisposable
	{
		void Execute(string key, object message);
	}
}
