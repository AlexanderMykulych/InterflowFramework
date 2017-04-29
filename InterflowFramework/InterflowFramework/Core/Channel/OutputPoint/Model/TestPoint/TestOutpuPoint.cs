using InterflowFramework.Core.Channel.OutputPoint.Const;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterflowFramework.Core.Channel.OutputPoint.Model.TestPoint
{
	public class TestOutpuPoint : BaseOutputPoint
	{
		public override void Push(object message)
		{
			Subscriber.Execute(OutputPointEvent.OnMessageRecived, message);
		}
	}
}
