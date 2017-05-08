using InterflowFramework.Core.Factory.PointFactory;
using InternalFramework.Nancy.InputPoint;
using Nancy.Hosting.Self;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternalFramework.NencyInPoint
{
	class Program
	{
		static void Main(string[] args)
		{
			PointFactory.In("nancy", () => new NancyInputPoint("Get", "/test", _ => "Hello Test!"));
			PointFactory.In("nancy", () => new NancyInputPoint("Get", "/", _ => "Hello!"));
			var inPoint = new NancyInputPoint("Get", "/new", _ => "Hello New!");
			PointFactory.In("nancy", () => inPoint);
			var hostConfig = new HostConfiguration();
			hostConfig.UrlReservations.CreateAutomatically = true;
			using (var host = new NancyHost(hostConfig, new Uri("http://localhost:8888/nancy/"))) {
				host.Start();
				try
				{
					System.Diagnostics.Process.Start("http://localhost:8888/nancy/");
				}
				catch (Exception)
				{
				}
				Console.WriteLine("Go");
				Console.ReadKey();
				Console.WriteLine("New");
				inPoint.Action = _ => "Hello new 2!";
				PointFactory.Enable("nancy");
				Console.ReadKey();
			}
		}
	}
}
