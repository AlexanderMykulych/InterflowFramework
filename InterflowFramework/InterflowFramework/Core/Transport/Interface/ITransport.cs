using InterflowFramework.Core.Channel.Enabler.Interface;
using InterflowFramework.Core.Channel.Pusher.Interface;
using InterflowFramework.Core.Channel.Subscriber.Interface;
using InterflowFramework.Core.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterflowFramework.Core.Channel.Transport
{
	public interface ITransport : IPusher, IEnabler, ISubscriber, IDisposable
	{
		void Response(object message);
	}
}
