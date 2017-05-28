using InterflowFramework.Core.Channel;
using InterflowFramework.Core.Channel.OutputPoint.Const;
using InterflowFramework.Core.Channel.OutputPoint.Model.TestPoint;
using InterflowFramework.Core.Factory.PointFactory;
using InterflowFrameworkTelegram.InputPoint;
using InterflowFrameworkTelegram.InputPoint.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterflowFramework.Core.Factory.Channel;
using InterflowFramework.Core.Channel.Transport.Model.Inline;

namespace InterflowFramework.ChannelCreatorRunner
{
	class Program
	{
		static void Main(string[] args)
		{
			RegisterPoint();
			using (var channel = RegisterChannel()) {
				channel.Enable();
				using (var channel2 = RegisterChannel())
				{
					channel2.Enable();
					Console.ReadKey();
				}
			}
		}

		private static IChannel RegisterChannel()
		{
			return new MessageChannelCreator()
				.In("telegram")
				.Out("console")
				.Transport(new InlineTransport())
				.Create();
		}

		private static void RegisterPoint()
		{
			var outPoint = new TestOutpuPoint();
			outPoint.On(OutputPointEvent.OnMessageRecived, obj => Console.WriteLine(obj));
			PointFactory.In("telegram", () => new TelegramInputPoint(new TelegramInputPointConfig("368717724:AAFkTcAtUnKXihwTiHqwAuCdXEvN6USz4Pk")), true);
			PointFactory.Out("console", () => outPoint);
		}
	}
}
