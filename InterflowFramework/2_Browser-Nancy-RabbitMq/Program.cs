using InterflowFramework.Core.Factory.Channel;
using InterflowFramework.Core.Factory.PointFactory;
using InterflowFramework.RabbitMq.Transport;
using InternalFramework.Nancy.InputPoint;
using InternalFramework.Nancy.Packer;
using Nancy.Hosting.Self;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2_Browser_Nancy_RabbitMq
{
	class Program
	{
		static void Main(string[] args)
		{
			PointFactory.In("nancy_first", () => new NancyInputPoint("Get", "/first/{id}", null, false));

			new PackedMessageChannelCreator()
				.In("nancy_first")
				.Packers(responseUnpacker: new NancyUnpacker())
				.Transport(new RabbitMqTransport("host=localhost", "demo2_first", true, false, true))
				.Create()
				.Enable();

			using (var host = new NancyHost(new Uri("http://localhost:8888/nancy/")))
			{
				host.Start();

				Console.WriteLine("Press Any key to stop.");
				Console.ReadKey();
			}
		}
	}
}
