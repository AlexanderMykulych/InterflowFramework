using InterflowFramework.Core.Message.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterflowFramework.Core.Message.Interface;
using Newtonsoft.Json.Linq;

namespace InterflowFramework.Core.Message.Model.Json.Executor
{
	public class JsonJPathRequestExecutor : BaseRequestExecutor
	{
		public override IEnumerable<object> GetMathes(string request, IMessage message)
		{
			if (message is JsonMessage)
			{
				var jsonMessage = (JsonMessage)message;
				var jToken = jsonMessage.JToken;
				if (jToken != null)
				{
					var result = jToken.SelectTokens(request);
					if (result != null)
					{
						return result.Select(x => x is JValue ? ((JValue)x).Value : null).Where(x => x != null);
					}
				}
			}
			return new List<object>();
		}

		protected override IMessageRequestResponse CreateResponse()
		{
			return new JsonMessageRequestResponse();
		}
	}
}
