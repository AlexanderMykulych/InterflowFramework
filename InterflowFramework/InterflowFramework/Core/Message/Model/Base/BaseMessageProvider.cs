using InterflowFramework.Core.Message.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterflowFramework.Core.Message.Model.Base
{
	public abstract class BaseMessageProvider : IMessageProvider
	{
		public virtual IMessage Create(object inputObject)
		{
			if (inputObject == null)
			{
				return CreateTemplateMessage();
			}
			return null;
		}
		public abstract IMessage CreateTemplateMessage();
	}
}
