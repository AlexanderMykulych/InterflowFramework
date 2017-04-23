using InterflowFramework.Core.Message.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterflowFramework.Core.Message.Model.Xml
{
	public class XmlMessageProvider : BaseMessageProvider
	{
		public override IMessage Create(object inputObject)
		{
			var message = base.Create(inputObject);
			if (message == null)
			{
				if (inputObject is string)
				{
					message = new XmlMessage((string)inputObject);
				}
				message = new XmlMessage(inputObject.ToString());
			}
			return message;
		}

		public override IMessage CreateTemplateMessage()
		{
			return new XmlMessage(string.Empty);
		}
	}
}
