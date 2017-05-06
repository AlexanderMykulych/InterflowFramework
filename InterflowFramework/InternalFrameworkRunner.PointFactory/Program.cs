using InterflowFramework.Core.Channel.Model;
using InterflowFramework.Core.Channel.OutputPoint.Model.TestPoint;
using InterflowFramework.Core.Channel.Transport.Model.Inline;
using InterflowFramework.Core.Factory.PointFactory;
using InterflowFrameworkTelegram.InputPoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternalFrameworkRunner
{
	class Program
	{
		static void Main(string[] args)
		{
			RegisterPoint();
			var channel = new MessageChannel()
			{
				Transport = new InlineTransport(),
				OutputPoints = PointFactory.GetOutPoints("test"),
				InputPoints = PointFactory.GetInPoints("test")
			};
			channel.Enable();
		}

		private static void RegisterPoint()
		{
			PointFactory.In("test", () => new TelegramInputPoint(new InterflowFrameworkTelegram.InputPoint.Config.TelegramInputPointConfig("")));
			PointFactory.Out("test", () => new TestOutpuPoint());
		}
	}
}
