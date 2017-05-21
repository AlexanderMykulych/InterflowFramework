using _1_RabbitMq_Mapping_ElasticSearch.Mapping;
using Nest;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebJs_NancyOwin_ConsoleWebHost.Mapping.requestWillBeSent
{

	public class Rootobject
	{
		public string _index { get; set; }
		public string _type { get; set; }
		public string _id { get; set; }
		public int _score { get; set; }
		public _Source _source { get; set; }
	}

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
		public string documentURL { get; set; }
		public string frameId { get; set; }
		public Initiator initiator { get; set; }
		public string loaderId { get; set; }
		public Request request { get; set; }
		public string requestId { get; set; }
		[JsonProperty(PropertyName = "timestamp")]
		[JsonConverter(typeof(UnixDateTimeConverter))]
		public DateTime timestamp { get; set; }
		public string type { get; set; }
		[JsonProperty(PropertyName = "wallTime")]
		[JsonConverter(typeof(UnixDateTimeConverter))]
		public DateTime wallTime { get; set; }
	}

	public class Initiator
	{
		public Stack stack { get; set; }
		public string type { get; set; }
	}

	public class Stack
	{
		public List<Callframe> callFrames { get; set; }
	}

	public class Callframe
	{
		public int columnNumber { get; set; }
		public string functionName { get; set; }
		public int lineNumber { get; set; }
		public string scriptId { get; set; }
		public string url { get; set; }
	}

	public class Request
	{
		public Headers headers { get; set; }
		public string initialPriority { get; set; }
		public string method { get; set; }
		public string mixedContentType { get; set; }
		public string referrerPolicy { get; set; }
		public string url { get; set; }
		//[GeoPoint]
		//public GeoLocation Coordinates {
		//	get {
		//		return GeoUtil.GetByUrl(url);
		//	}
		//	set {
		//		return;
		//	}
		//}
		//[GeoPoint(IgnoreMalformed = true)]
		//public GeoLocation Coordinates2 {
		//	get {
		//		return GeoUtil.GetByUrl(url);
		//	}
		//	set {
		//		return;
		//	}
		//}
	}
	
	public class Headers
	{
		public string Origin { get; set; }
		public string Referer { get; set; }
		[JsonProperty("User-Agent")]
		public string UserAgent { get; set; }
	}

}
