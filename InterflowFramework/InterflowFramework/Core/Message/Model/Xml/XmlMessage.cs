using InterflowFramework.Core.Message.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterflowFramework.Core.Message.Interface;
using System.Xml;
using System.Xml.Linq;

namespace InterflowFramework.Core.Message.Model.Xml
{
	public class XmlMessage : BaseMessage
	{
		public string XmlText;
		public XDocument XmlDocument;
		public XmlMessage(string xml) {
			XmlText = xml;
			XmlDocument = XDocument.Parse(xml);
		}
		public override bool ValidateRequest(IMessageRequest request)
		{
			return request is XmlMessageRequest;
		}
	}
}
