using InterflowFramework.Core.Channel.OutputPoint.Const;
using InterflowFramework.Core.Channel.OutputPoint.Model;
using InternalFramework;
using InternalFramework.OutputPoint;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace InterflowFramework.HttpWeb.OutputPoint
{
	public class JsonPostServiceOutPoint : ResponseOutputPoint
	{
		private string _baseUri { get; set; }
		private JsonServiceClient _client { get; set; }

		public JsonPostServiceOutPoint(string baseUri): base() {
			_baseUri = baseUri;
			_client = new JsonServiceClient(baseUri);
			Initialize(OnMessage);
		}
		public virtual object OnMessage(object message) {
			object requestObject = message;
			if (message is MessagePackage)
			{
				requestObject = ((MessagePackage)message).Object;
			}
			return _client.Post(requestObject);
		}
	}
}
