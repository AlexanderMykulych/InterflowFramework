using Elasticsearch.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runner.ElasticSearchLowLevelClient
{
	class Program
	{
		public class Person
		{
			public string FirstName { get; set; }
			public string LastName { get; set; }
		}
		static void Main(string[] args)
		{
			var p1 = new Person()
			{
				FirstName = "a",
				LastName = "B"
			};
			var client = new ElasticLowLevelClient();
			var indexResponse = client.Index<byte[]>("default", "person", "1", p1);

			var indexResponse2 = client.Index<byte[]>("tree", "person", "2", @"{""FirstName"": ""c"", ""LastName"": ""n""}");
			byte[] responseBytes2 = indexResponse2.Body;

			client.Index<byte[]>("people", "person", "3", "{\"type\":\"to-server\",\"info\":{\"type\":\"subscriber\",\"key\":\"Test\",\"text\":\"\"}}");
			Console.ReadKey();
		}
	}
}
