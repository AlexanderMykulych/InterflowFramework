using InterflowFramework.Core.Channel.OutputPoint.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace InternalFramework.ElasticSearch.OutputPoint
{
	public class ElasticSearchOutputPoint : BaseOutputPoint
	{
		public string ConnectionString;
		public string Index;
		public string Type;
		private ElasticLowLevelClient _client;
		public ElasticLowLevelClient Client {
			get {
				if (_client == null) {
					if (string.IsNullOrEmpty(ConnectionString))
					{
						_client = new ElasticLowLevelClient();
					}
					else
					{
						_client = new ElasticLowLevelClient(new ConnectionConfiguration(new Uri(ConnectionString)));
					}
				}
				return _client;
			}
		}
		public ElasticSearchOutputPoint(string connectionString = null, string index = "default", string type = "default") {
			ConnectionString = connectionString;
			Index = index;
			Type = type;
		}
		public override void Push(object message)
		{
			if(!IsEnabled) {
				return;
			}
			Client.Index<byte[]>(Index, Type, Guid.NewGuid().ToString(), message);
		}
	}
}
