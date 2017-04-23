using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterflowFramework.Core.Message.Interface;
using InterflowFramework.Core.Message.Model.Base;
using HtmlAgilityPack;

namespace InterflowFramework.Core.Message.Model.Html
{
	public class HtmlMessage : BaseMessage
	{
		public string HtmlText;
		public HtmlDocument HtmlDocument;
		public HtmlMessage(string html) {
			HtmlText = html;
			HtmlDocument = new HtmlDocument();
			HtmlDocument.LoadHtml(html);
		}

		public override bool ValidateRequest(IMessageRequest request)
		{
			return request is HtmlMessageRequest;
		}
	}
}
