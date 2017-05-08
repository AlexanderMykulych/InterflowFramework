using InterflowFramework.Core.Channel.InputPoint;
using InterflowFramework.Core.Channel.Model;
using InterflowFramework.Core.Channel.OutputPoint;
using InterflowFramework.Core.Channel.OutputPoint.Const;
using InterflowFramework.Core.Channel.OutputPoint.Model.TestPoint;
using InterflowFramework.Core.Message.Model.Text;
using InternalFramework.NetMQ.Transport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMqRunnerServer
{
	class Program
	{
		static void Main(string[] args)
		{
			var OutPoint = new TestOutpuPoint();
			OutPoint.On(OutputPointEvent.OnMessageRecived, onMessage);
			var channel = new MessageChannel()
			{
				Transport = new NetMqListenerTransport<TextMessage>(@"tcp://localhost:56001"),
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
			}
		}
	}
}
