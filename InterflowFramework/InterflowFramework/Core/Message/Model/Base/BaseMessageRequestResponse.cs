using InterflowFramework.Core.Message.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterflowFramework.Core.Message.Model.Base
{
	public class BaseMessageRequestResponse : IMessageRequestResponse
	{
		protected Func<IEnumerable<object>> Setter;
		public virtual IEnumerable<object> GetResponse()
		{
			return Setter();
		}
		public virtual void SetLazy(Func<IEnumerable<object>> setter)
		{
			Setter = setter;
		}
	}
}
