using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebJs_NancyOwin_ConsoleWebHost.Mapping.requestServedFromCache
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
		public string requestId { get; set; }
	}

}
