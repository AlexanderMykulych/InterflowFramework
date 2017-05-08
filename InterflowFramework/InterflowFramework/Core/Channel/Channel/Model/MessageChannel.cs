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
using InterflowFramework.Core.Channel.Identified;
using InterflowFramework.Core.Channel.OutputPoint.Const;

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
			point.On(OutputPointEvent.OnResponse, message => Transport.Response(message));
		}

		protected override void ConfigurateTransport()
		{
			Transport
				.On(TransportEvent.onMessage, OnTransportMessage)
				.On(TransportEvent.onConnect, x => Subscriber.Execute(TransportEvent.onConnect, x))
				.On(TransportEvent.onDisconnect, OnTransportDisconnect)
				.On(TransportEvent.onResponse, OnTransportResponse);
		}

		private void OnTransportResponse(object obj)
		{
			if(obj is IIdentified) {
				var identified = (IIdentified)obj;
				var inPoint = InputPoints.FirstOrDefault(x => x.GetId() == identified.GetId());
				if (inPoint != null)
				{
					inPoint.Message(obj);
					return;
				}
				var outPoint = OutputPoints.FirstOrDefault(x => x.GetId() == identified.GetId());
				if (outPoint != null)
				{
					outPoint.Message(obj);
					return;
				}
			}
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
