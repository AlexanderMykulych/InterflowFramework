using InterflowFramework.Core.Channel.OutputPoint.Model;
using InterflowFramework.Core.Channel.Transport.Model.Inline;
using InterflowFramework.Core.Factory.Channel;
using InterflowFramework.Core.Factory.PointFactory;
using InternalFramework.Nancy.InputPoint;
using InternalFramework.Nancy.OutputPoint;
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
			PointFactory.In("nancy_Http", () => new NancyInputPoint("Post", "/"));
			PointFactory.Out("nancy_Http", () => new ResponseOutputPoint(OnHttpMessage));
			PointFactory.In("nancy_WebSocket", () => new NancyInputPoint("Post", "/websocket"));
			PointFactory.Out("nancy_WebSocket", () => new InlineOutputPoint(OnWebSocketMessage));
			var channelWebsocket = new ChannelCreator()
				.Is("Demo1_WebSocket")
				.InOut("nancy_WebSocket")
				.Transport(new InlineTransport())
				.Create();

			var channelHttp = new ChannelCreator()
				.Is("Demo1_Http")
				.InOut("nancy_Http")
				.Transport(new InlineTransport())
				.Create();

			var url = "http://+:8081";

			using (WebApp.Start<Startup>(url))
			{
				channelHttp.Enable();
				channelWebsocket.Enable();
				Console.WriteLine("Running on {0}", url);
				Console.WriteLine("Press enter to exit");
				Console.ReadLine();
			}
			channelHttp.Dispose();
			channelWebsocket.Dispose();
		}
		public static object OnHttpMessage(object message)
		{
			Console.WriteLine("OnHttpMessage" + message.ToString());
			return "Hello!";
		}
		public static void OnWebSocketMessage(object message)
		{
			Console.WriteLine("OnWebSocketMessage" + message.ToString());
		}
	}

	public class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			app.UseNancy();
		}
	}
}
