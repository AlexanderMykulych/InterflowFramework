using InterflowFramework.Core.Channel.Transport.Model.Inline;
using InterflowFramework.Core.Factory.Channel;
using InterflowFramework.Core.Factory.PointFactory;
using InternalFramework.Nancy.InputPoint;
using InternalFramework.Nancy.OutputPoint;
using Nancy.Hosting.Self;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterflowFramework.NancyReturnOutPoint
{
	class Program
	{
		static void Main(string[] args)
		{
			PointFactory.In("nancy", () => new NancyInputPoint("Get", "/test", null));
			PointFactory.Out("nancy", () => new ReturnOutputPoint("Hello world!!!"));
			var channel = new ChannelCreator()
				.Is("Test")
				.In("nancy")
				.Out("nancy")
				.Transport(new InlineTransport())
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
				channel.Enable();
				Console.WriteLine("Go");
				Console.ReadKey();
				Console.WriteLine("New");
				Console.ReadKey();
			}
		}
	}
}
