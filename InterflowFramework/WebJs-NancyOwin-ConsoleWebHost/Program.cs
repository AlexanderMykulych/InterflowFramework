using InterflowFramework.Core.Channel.OutputPoint.Model;
using InterflowFramework.Core.Channel.Transport.Model.Inline;
using InterflowFramework.Core.Factory.Channel;
using InterflowFramework.Core.Factory.PointFactory;
using InterflowFramework.RabbitMq.Transport;
using InternalFramework.Fleck.InputPoint;
using Microsoft.Owin.Hosting;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebJs_NancyOwin_ConsoleWebHost
{
	//Name = Demo1
	class Program
	{
		static void Main(string[] args)
		{
			PointFactory.In("fleck", () => new FleckInputPoint("ws://0.0.0.0:8081"));

			var channel = new MessageChannelCreator()
				.InOut("fleck")
				.Transport(new RabbitMqTransport("host=localhost", "network", pushToRabbitEnable: true, subscribeRabbitEnable: false, catchResponse: false))
				.Create();
			channel.Enable();
			Console.WriteLine("Go!");
			Console.ReadKey();
		}
	}
}
