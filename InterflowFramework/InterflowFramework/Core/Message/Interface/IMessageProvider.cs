using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterflowFramework.Core.Message.Interface
{
	public interface IMessageProvider
	{
		IMessage CreateTemplateMessage();
		IMessage Create(Object inputObject);
	}
}
