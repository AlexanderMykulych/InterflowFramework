using InterflowFramework.Core.Message.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterflowFramework.Core.Message.Interface;
using Newtonsoft.Json.Linq;

namespace InterflowFramework.Core.Message.Model.Json
{
	public class JsonMessage : BaseMessage
	{
		public string JsonText;
		public JToken JToken;
		public JsonMessage(string json) {
			JsonText = json;
			JToken = JToken.Parse(json);
		}
		public override bool ValidateRequest(IMessageRequest request)
		{
			return request is JsonMessageRequest;
		}
	}
}
