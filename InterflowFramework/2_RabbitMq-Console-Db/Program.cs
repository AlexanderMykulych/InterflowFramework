using InterflowFramework.Core.Factory.Channel;
using InterflowFramework.Core.Factory.PointFactory;
using InterflowFramework.RabbitMq.Transport;
using InternalFramework.Nancy.InputPoint;
using InternalFramework.Nancy.OutputPoint;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _2_RabbitMq_Console_Db
{
	class Program
    {
		static void Main(string[] args)
		{
			PointFactory.Out("response", () => new ResponseOutputPoint(OnMessage));

			new MessageChannelCreator()
				.Transport(new RabbitMqTransport("host=localhost", "demo2_first", false, true))
				.Out("response")
				.Create()
				.Enable();

			Console.WriteLine("Press Any key to stop.");
			Console.ReadKey();
		}
		public static object OnMessage(object message)
		{
			if (message is MessagePackage)
			{
				var dict = ((MessagePackage)message).Object as Dictionary<string, object>;
				return new NancyResponse()
				{
					ContentType = "text/html",
					Content = string.Format("<h1>HTML ХУЙ - {0}!</h2>", dict.First().Value)
				};
			}
			return "Pong";
		}
	}
}