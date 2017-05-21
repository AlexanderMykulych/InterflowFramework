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
		public string Index;
		public string Type;
		public bool IsLowLevelClient;
		private ElasticLowLevelClient _client;
		public ElasticLowLevelClient LowLevelClient {
			get {
				if (_client == null)
				{
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
		private ElasticClient _highLevelClient;
		public ElasticClient HighLevelClient {
			get {
				if (_highLevelClient == null)
				{
					if (string.IsNullOrEmpty(ConnectionString))
					{
						_highLevelClient = new ElasticClient();
					}
					else
					{
						_highLevelClient = new ElasticClient(new ConnectionSettings(new Uri(ConnectionString)).DefaultIndex(Index));
					}
				}
				return _highLevelClient;
			}
		}
		public ElasticSearchOutputPoint(string connectionString = null, string index = "default", string type = "default", bool lowLevelClient = true)
		{
			ConnectionString = connectionString;
			Index = index;
			Type = type;
			IsLowLevelClient = lowLevelClient;
		}
		public override void Push(object message)
		{
			if (!IsEnabled)
			{
				return;
			}
			if (IsLowLevelClient)
			{
				LowLevelClient.Index<byte[]>(Index, Type, Guid.NewGuid().ToString(), message);
			}
			else
			{
				HighLevelClient.Index(message);
			}
		}
	}
}
