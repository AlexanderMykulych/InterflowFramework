using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterflowFramework.Core.Channel.InputPoint;
using InterflowFramework.Core.Channel.OutputPoint;
using InterflowFramework.Core.Channel.InputPoint.Const;
using InterflowFramework.Core.Channel.Transport.Const;
using InterflowFramework.Core.Channel.Subscriber.Interface;
using InterflowFramework.Core.LinqExtension;

namespace InterflowFramework.Core.Channel.Model
{
	public class MessageChannel : BaseChannel
	{
		
		protected override void ConfigurateInputPointSubscribe(IInputPoint point)
		{
			point.On(InputPointEvent.onMessage, onInputPointMessage);
		}

		private void onInputPointMessage(object obj)
		{
			Transport.Push(obj);
		}

		protected override void ConfigurateOutputPointSubscribe(IOutputPoint point)
		{
		}

		protected override void ConfigurateTransport()
		{
			Transport
				.On(TransportEvent.onMessage, OnTransportMessage)
				.On(TransportEvent.onConnect, x => Subscriber.Execute(TransportEvent.onConnect, x))
				.On(TransportEvent.onDisconnect, OnTransportDisconnect);
		}

		protected virtual void OnTransportMessage(object message) {
			OutputPoints.ForEach(x => x.Push(message));
			Subscriber.Execute(TransportEvent.onMessage, message);
		}
		protected virtual void OnTransportDisconnect(object message) {
			Disable();
			Subscriber.Execute(TransportEvent.onDisconnect, message);
		}
	}
}
