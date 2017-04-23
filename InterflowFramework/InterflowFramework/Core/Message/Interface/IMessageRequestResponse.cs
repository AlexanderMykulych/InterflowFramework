using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterflowFramework.Core.Message.Interface
{
	public interface IMessageRequestResponse
	{
		IEnumerable<object> GetResponse();
		void SetLazy(Func<IEnumerable<object>> setter);
	}
}
