using InterflowFramework.Core.Channel.Transport.Model.Inline;
using InterflowFramework.Core.Factory.Channel;
using InterflowFramework.Core.Factory.PointFactory;
using InterflowFramework.RabbitMq.Transport;
using InternalFramework.Nancy.InputPoint;
using InternalFramework.Nancy.OutputPoint;
using Nancy.Hosting.Self;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterflowFramework.NancyRebbitTransport
{
	class Program
	{
		static void Main(string[] args)
		{
			PointFactory.In("nancy", () => new NancyInputPoint("Get", "/test", null));
			var channelInput = new MessageChannelCreator()
				.Is("Test")
				.In("nancy")
				.Transport(new RabbitMqTransport("host=localhost", "test", true, false, true))
				.Create();

			var hostConfig = new HostConfiguration();
			hostConfig.UrlReservations.CreateAutomatically = true;
			using (var host = new NancyHost(hostConfig, new Uri("http://localhost:8888/nancy/")))
			{
				host.Start();
				try
				{
					System.Diagnostics.Process.Start("http://localhost:8888/nancy/test");
				}
				catch (Exception)
				{
				}
				channelInput.Enable();
				Console.WriteLine("Go");
				Console.ReadKey();
				Console.WriteLine("New");
				Console.ReadKey();
			}
		}
	}
}
