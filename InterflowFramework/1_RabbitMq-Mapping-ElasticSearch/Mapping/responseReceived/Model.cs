using _1_RabbitMq_Mapping_ElasticSearch.Mapping;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebJs_NancyOwin_ConsoleWebHost.Mapping.responseReceived
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
		public string frameId { get; set; }
		public string loaderId { get; set; }
		public string requestId { get; set; }
		public Response response { get; set; }
		[JsonProperty(PropertyName = "timestamp")]
		[JsonConverter(typeof(UnixDateTimeConverter))]
		public DateTime timestamp { get; set; }
		public string type { get; set; }
	}

	public class Response
	{
		public int connectionId { get; set; }
		public bool connectionReused { get; set; }
		public int encodedDataLength { get; set; }
		public bool fromDiskCache { get; set; }
		public bool fromServiceWorker { get; set; }
		public Headers headers { get; set; }
		public string mimeType { get; set; }
		public string protocol { get; set; }
		public string remoteIPAddress { get; set; }
		public int remotePort { get; set; }
		public Requestheaders requestHeaders { get; set; }
		public Securitydetails securityDetails { get; set; }
		public string securityState { get; set; }
		public int status { get; set; }
		public string statusText { get; set; }
		public Timing timing { get; set; }
		public string url { get; set; }
	}

	public class Headers
	{
		public string accesscontrolallowcredentials { get; set; }
		public string accesscontrolalloworigin { get; set; }
		public string contentlength { get; set; }
		public string contenttype { get; set; }
		public DateTime date { get; set; }
		public string p3p { get; set; }
		public string server { get; set; }
		public string setcookie { get; set; }
		public string status { get; set; }
	}

	public class Requestheaders
	{
		public string authority { get; set; }
		public string method { get; set; }
		public string path { get; set; }
		public string scheme { get; set; }
		public string accept { get; set; }
		public string acceptencoding { get; set; }
		public string acceptlanguage { get; set; }
		public string cookie { get; set; }
		public string origin { get; set; }
		public string referer { get; set; }
		public string useragent { get; set; }
	}

	public class Securitydetails
	{
		public int certificateId { get; set; }
		public string cipher { get; set; }
		public string issuer { get; set; }
		public string keyExchange { get; set; }
		public string keyExchangeGroup { get; set; }
		public string protocol { get; set; }
		public List<string> sanList { get; set; }
		public object[] signedCertificateTimestampList { get; set; }
		public string subjectName { get; set; }
		public int validFrom { get; set; }
		public int validTo { get; set; }
	}

	public class Timing
	{
		public int connectEnd { get; set; }
		public int connectStart { get; set; }
		public int dnsEnd { get; set; }
		public int dnsStart { get; set; }
		public int proxyEnd { get; set; }
		public int proxyStart { get; set; }
		public int pushEnd { get; set; }
		public int pushStart { get; set; }
		public float receiveHeadersEnd { get; set; }
		public float requestTime { get; set; }
		public float sendEnd { get; set; }
		public float sendStart { get; set; }
		public int sslEnd { get; set; }
		public int sslStart { get; set; }
		public int workerReady { get; set; }
		public int workerStart { get; set; }
	}

}
