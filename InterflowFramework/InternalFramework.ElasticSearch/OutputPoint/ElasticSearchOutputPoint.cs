using InterflowFramework.Core.Channel.OutputPoint.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Nest;

namespace InternalFramework.ElasticSearch.OutputPoint
{
	public class ElasticSearchOutputPoint : BaseOutputPoint
	{
		public string ConnectionString;
		public string DefaultIndex;
		private ElasticClient _client;
		public ElasticClient Client {
			get {
				if (_client == null) {
					_client = new ElasticClient(new ConnectionSettings(new Uri(ConnectionString)));
				}
				return _client;
			}
		}
		public ElasticSearchOutputPoint(string connectionString = @"http://localhost:9200", string defaultIndex = "my-application") {
			ConnectionString = connectionString;
			DefaultIndex = defaultIndex;
		}
		public override void Push(object message)
		{
			if(IsEnabled) {
				return;
			}
			Client.Index(message);
		}
	}
}
