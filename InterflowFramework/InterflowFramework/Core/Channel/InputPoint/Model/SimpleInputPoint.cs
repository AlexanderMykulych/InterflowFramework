using InterflowFramework.Core.Channel.InputPoint.Const;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterflowFramework.Core.Channel.InputPoint.Model
{
	public class SimpleInputPoint : BaseInputPoint
	{
		public override void Push(object message)
		{
			Subscriber.Execute(InputPointEvent.onMessage, message);
		}
	}
}
