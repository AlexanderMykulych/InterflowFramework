using _1_RabbitMq_Mapping_ElasticSearch.Mapping;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebJs_NancyOwin_ConsoleWebHost.Mapping.dataReceived
{

	public class _Source
	{
		public string type { get; set; }
		public Info info { get; set; }
	}

	public class Info
	{
		public string type { get; set; }
		public string key { get; set; }
		public Text text { get; set; }
	}

	public class Text
	{
		public int id { get; set; }
		public Result result { get; set; }
	}

	public class Result
	{
		public string subject { get; set; }
		public string message { get; set; }
		public Data data { get; set; }
	}

	public class Data
	{
		public int dataLength { get; set; }
		public int encodedDataLength { get; set; }
		public string requestId { get; set; }
		[JsonProperty(PropertyName = "timestamp")]
		[JsonConverter(typeof(UnixDateTimeConverter))]
		public DateTime timestamp { get; set; }
	}

}
