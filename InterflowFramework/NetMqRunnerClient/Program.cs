using InterflowFramework.Core.Channel.InputPoint;
using InterflowFramework.Core.Channel.Model;
using InterflowFramework.Core.Channel.OutputPoint;
using InterflowFramework.Core.Channel.OutputPoint.Const;
using InterflowFramework.Core.Channel.OutputPoint.Model.TestPoint;
using InterflowFramework.Core.Message.Model.Text;
using InterflowFrameworkTelegram.InputPoint;
using InterflowFrameworkTelegram.InputPoint.Config;
using InternalFramework.NetMQ.Transport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMqRunnerClient
{
	class Program
	{
		static void Main(string[] args)
		{
			var OutPoint = new TestOutpuPoint();
			OutPoint.On(OutputPointEvent.OnMessageRecived, onMessage);
			var channel = new MessageChannel()
			{
				Transport = new NetMqPublisherTransport(@"tcp://localhost:56001"),
				InputPoints = new List<IInputPoint>() {
					new TelegramInputPoint(new TelegramInputPointConfig("368717724:AAFkTcAtUnKXihwTiHqwAuCdXEvN6USz4Pk"))
				},
				OutputPoints = new List<IOutputPoint>() {
					OutPoint
				}
			};
			channel.Enable();
			Console.ReadKey();
		}

		private static void onMessage(object obj)
		{
			if(obj is TextMessage) {
				Console.WriteLine(((TextMessage)obj).Text);
			}
		}
	}
}
