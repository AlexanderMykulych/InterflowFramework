using InterflowFramework.Core.Channel.Transport.Const;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterflowFramework.Core.Channel.Transport.Model.Inline
{
	public class InlineTransport : BaseTransport
	{
		public override void Push(object message)
		{
			Subscriber.Execute(TransportEvent.onMessage, message);
		}

		public override void Response(object message)
		{
			Subscriber.Execute(TransportEvent.onResponse, message);
		}
	}
}
