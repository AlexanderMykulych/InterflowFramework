using InterflowFramework.Core.Channel.InputPoint;
using InterflowFramework.Core.Channel.Model;
using InterflowFramework.Core.Channel.OutputPoint;
using InterflowFramework.Core.Channel.OutputPoint.Const;
using InterflowFramework.Core.Channel.OutputPoint.Model.TestPoint;
using InterflowFramework.Core.Message.Model.Text;
using InterflowFramework.RabbitMq.Transport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterflowFrameworkRunner.RabbitMqOutput
{
	class Program
	{
		static void Main(string[] args)
		{
			var OutPoint = new TestOutpuPoint();
			OutPoint.On(OutputPointEvent.OnMessageRecived, onMessage);
			var channel = new MessageChannel()
			{
				Transport = new RabbitMqTransport(@"host=localhost", "test", false),
				OutputPoints = new List<IOutputPoint>() {
					OutPoint
				}
			};
			channel.Enable();
			Console.ReadKey();
		}
		private static void onMessage(object obj)
		{
			if (obj is TextMessage)
			{
				Console.WriteLine(((TextMessage)obj).Text);
			} else if(obj is string) {
				Console.WriteLine((string)obj);
			}
		}
	}
}
