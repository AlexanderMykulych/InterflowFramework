using InterflowFramework.Core.Channel.InputPoint;
using InterflowFramework.Core.Channel.InputPoint.Const;
using InterflowFramework.Core.Channel.InputPoint.Model;
using InterflowFramework.Core.Channel.Model;
using InterflowFramework.Core.Channel.OutputPoint;
using InterflowFramework.Core.Channel.OutputPoint.Const;
using InterflowFramework.Core.Channel.OutputPoint.Model.TestPoint;
using InterflowFramework.Core.Message.Model.Text;
using InterflowFramework.RabbitMq.Transport;
using InterflowFrameworkTelegram.InputPoint;
using InterflowFrameworkTelegram.InputPoint.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterflowFrameworkRunner.RabbitMqInput
{
	class Program
	{
		static void Main(string[] args)
		{
			var OutPoint = new TestOutpuPoint();
			OutPoint.On(OutputPointEvent.OnMessageRecived, onMessage);
			var loadInPoint = new LoadTestInputPoint(10);
			loadInPoint.On(InputPointEvent.onMessage, onInputMessage);
			var channel = new MessageChannel()
			{
				Transport = new RabbitMqTransport(@"host=localhost", "test", true, false),
				OutputPoints = new List<IOutputPoint>() {
					OutPoint
				},
				InputPoints = new List<IInputPoint>() {
					loadInPoint
				}
			};
			Console.WriteLine("Press any key to start");
			Console.ReadKey();
			Console.WriteLine("Go");
			channel.Enable();
			Console.ReadKey();
		}

		private static void onInputMessage(object obj)
		{
			if(obj is string) {
				Console.WriteLine("send: " + (string)obj);
			}
		}

		private static void onMessage(object obj)
		{
			if (obj is TextMessage)
			{
				Console.WriteLine(((TextMessage)obj).Text);
			}
		}
	}
}
