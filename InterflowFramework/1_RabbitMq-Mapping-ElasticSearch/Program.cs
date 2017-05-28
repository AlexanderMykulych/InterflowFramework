using InterflowFramework.Core.Channel.InputPoint.Model;
using InterflowFramework.Core.Channel.OutputPoint.Model;
using InterflowFramework.Core.Channel.Transport.Model.Inline;
using InterflowFramework.Core.Factory.Channel;
using InterflowFramework.Core.Factory.PointFactory;
using InterflowFramework.RabbitMq.Transport;
using InternalFramework.ElasticSearch.OutputPoint;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1_RabbitMq_Mapping_ElasticSearch
{
	class Program
	{
		static void Main(string[] args)
		{
			PointFactory.Out("elasticSearch", () => new ElasticSearchOutputPoint("http://localhost:9200", "statistic", "network"));
			PointFactory.In("simple", () => new SimpleInputPoint());
			PointFactory.Out("simple", () => new InlineOutputPoint(obj => PointFactory.PushIn("simple", MappThatObject(obj), x => x != null)));

			new MessageChannelCreator()
				.Transport(new InlineTransport())
				.In("simple")
				.Out("elasticSearch")
				.Create()
				.Enable();

			new MessageChannelCreator()
				.Transport(new RabbitMqTransport("host=localhost", "network", false, true))
				.Out("simple")
				.Create()
				.Enable();

			Console.ReadKey();
		}

		public static object MappThatObject(object obj) {
			if(obj is string) {
				var jObject = JObject.Parse((string)obj);
				if (jObject != null) {
					var jToken = jObject.SelectToken("info.text.result.message");
					if(jToken != null) {
						var message = jToken.Value<string>();
						switch(message) {
							case "Network.dataReceived":
								return jObject.ToObject<WebJs_NancyOwin_ConsoleWebHost.Mapping.dataReceived._Source>();
							case "Network.loadingFailed":
								return jObject.ToObject<WebJs_NancyOwin_ConsoleWebHost.Mapping.loadingFailed._Source>();
							case "Network.loadingFinished":
								return jObject.ToObject<WebJs_NancyOwin_ConsoleWebHost.Mapping.loadingFinished._Source>();
							case "Network.requestServedFromCache":
								return jObject.ToObject<WebJs_NancyOwin_ConsoleWebHost.Mapping.requestServedFromCache._Source>();
							case "Network.requestWillBeSent":
								return jObject.ToObject<WebJs_NancyOwin_ConsoleWebHost.Mapping.requestWillBeSent._Source>();
							case "Network.responseReceived":
								return jObject.ToObject<WebJs_NancyOwin_ConsoleWebHost.Mapping.responseReceived._Source>();
						}
					}
				}
			}
			return null;
		}
	}
}
