using InterflowFramework.Core.Channel.OutputPoint.Model;
using InterflowFramework.Core.Channel.Transport.Model.Inline;
using InterflowFramework.Core.Factory.Channel;
using InterflowFramework.Core.Factory.PointFactory;
using InterflowFramework.RabbitMq.Transport;
using InternalFramework.Fleck.InputPoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterflowFrameworkRunner.Fleck
{
	class Program
	{
		static void Main(string[] args)
		{
			PointFactory.In("fleck", () => new FleckInputPoint("ws://0.0.0.0:8081"));
			PointFactory.Out("fleck", () => new InlineOutputPoint(msg => Console.WriteLine(msg)));

			var channel = new ChannelCreator()
				.InOut("fleck")
				.Transport(new RabbitMqTransport("host=localhost", "network", true, true, false))
				.Create();
			channel.Enable();
			Console.WriteLine("Go!");
			Console.ReadKey();
		}
	}
}
