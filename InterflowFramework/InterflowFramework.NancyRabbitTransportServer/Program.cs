using InterflowFramework.Core.Factory.Channel;
using InterflowFramework.Core.Factory.PointFactory;
using InterflowFramework.RabbitMq.Transport;
using InternalFramework.Nancy.OutputPoint;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterflowFramework.NancyRabbitTransportServer
{
	class Program
	{
		static void Main(string[] args)
		{
			PointFactory.Out("nancy", () => new ReturnOutputPoint(Process.GetCurrentProcess().Id.ToString()));
			var channelOutput = new MessageChannelCreator()
				.Is("Test2")
				.Out("nancy")
				.Transport(new RabbitMqTransport("host=localhost", "test", false, true))
				.Create();
			channelOutput.Enable();
			Console.ReadKey();
		}
	}
}
