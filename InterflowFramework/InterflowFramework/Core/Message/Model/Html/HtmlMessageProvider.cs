using InterflowFramework.Core.Message.Interface;
using InterflowFramework.Core.Message.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterflowFramework.Core.Message.Model.Html
{
	public class HtmlMessageProvider : BaseMessageProvider
	{
		public override IMessage Create(object inputObject)
		{
			var message = base.Create(inputObject);
			if (message == null)
			{
				if (inputObject is string)
				{
					message = new HtmlMessage((string)inputObject);
				}
				message = new HtmlMessage(inputObject.ToString());
			}
			return message;
		}

		public override IMessage CreateTemplateMessage()
		{
			return new HtmlMessage(string.Empty);
		}
	}
}
