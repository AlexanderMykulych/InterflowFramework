using InterflowFramework.Core.Message.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterflowFramework.Core.Message.Interface;
using System.Xml.XPath;

namespace InterflowFramework.Core.Message.Model.Xml.Executor
{
	[XmlExecutor("xpath")]
	public class XmlXPathRequestExecutor : BaseRequestExecutor
	{
		public override IEnumerable<object> GetMathes(string request, IMessage message)
		{
			if (message is XmlMessage)
			{
				var xmlMessage = (XmlMessage)message;
				var xDocument = xmlMessage.XmlDocument;
				if (xDocument != null)
				{
					var result = xDocument.XPathSelectElements(request);
					if (result != null)
					{
						return result.Select(x => x.Value);
					}
				}
			}
			return new List<object>();
		}

		protected override IMessageRequestResponse CreateResponse()
		{
			return new XmlMessageRequestResponse();
		}
	}
}
