using InterflowFramework.Core.Message.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterflowFramework.Core.Message.Interface;
using HtmlAgilityPack;

namespace InterflowFramework.Core.Message.Model.Html.Executor
{
	public class HtmlXPathRequestExecutor : BaseRequestExecutor
	{
		public override IEnumerable<object> GetMathes(string request, IMessage message)
		{
			if(message is HtmlMessage) {
				var htmlMessage = (HtmlMessage)message;
				var htmlDocument = htmlMessage.HtmlDocument;
				if(htmlDocument != null) {
					var result = htmlDocument.DocumentNode.SelectNodes(request);
					if(result != null) {
						return result.Select(x => x.InnerText);
					}
				}
			}
			return new List<object>();
		}

		protected override IMessageRequestResponse CreateResponse()
		{
			return new HtmlMessageRequestResponse();
		}
	}
}
