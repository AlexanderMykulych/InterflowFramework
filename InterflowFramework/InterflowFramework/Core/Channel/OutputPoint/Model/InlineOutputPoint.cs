using InterflowFramework.Core.Channel.OutputPoint.Const;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterflowFramework.Core.Channel.OutputPoint.Model
{
	public class InlineOutputPoint : BaseOutputPoint
	{
		public InlineOutputPoint(params Action<object>[] actions) {
			actions.All(action => { On(OutputPointEvent.OnMessageRecived, action); return true; });
		}
		public override void Push(object message)
		{
			Subscriber.Execute(OutputPointEvent.OnMessageRecived, message);
		}
	}
}
