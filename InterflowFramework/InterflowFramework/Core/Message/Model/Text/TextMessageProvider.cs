using InterflowFramework.Core.Message.Interface;
using InterflowFramework.Core.Message.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterflowFramework.Core.Message.Model.Text
{
	public class TextMessageProvider : BaseMessageProvider
	{
		public override IMessage Create(object inputObject)
		{
			var message = base.Create(inputObject);
			if(message == null) {
				if (inputObject is string)
				{
					message = new TextMessage((string)inputObject);
				}
				message = new TextMessage(inputObject.ToString());
			}
			return message;
		}
		public override IMessage CreateTemplateMessage()
		{
			return new TextMessage(string.Empty);
		}
	}
}
