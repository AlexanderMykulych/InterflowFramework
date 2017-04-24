using InterflowFramework.Core.Message.Model.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterflowFramework.Core.Message.Model.Excell
{
	public class ExcellMessageProvider : BaseMessageProvider
	{
		public override IMessage Create(object inputObject)
		{
			var message = base.Create(inputObject);
			if (message == null)
			{
				if (inputObject is Stream)
				{
					message = new ExcellMessage((Stream)inputObject);
				}
			} else {
				message = CreateTemplateMessage();
			}
			return message;
		}

		public override IMessage CreateTemplateMessage()
		{
			return new ExcellMessage();
		}
	}
}
