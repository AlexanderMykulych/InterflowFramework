using InterflowFramework.Core.Channel.Enabler.Interface;
using InterflowFramework.Core.Channel.Pusher.Interface;
using InterflowFramework.Core.Channel.Subscriber.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterflowFramework.Core.Channel.OutputPoint
{
	public interface IOutputPoint: IPusher, IEnabler, ISubscriber, IDisposable
	{
	}
}
