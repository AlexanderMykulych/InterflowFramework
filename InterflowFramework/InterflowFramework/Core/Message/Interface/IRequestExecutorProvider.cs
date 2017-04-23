using InterflowFramework.Core.Message.Model.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterflowFramework.Core.Message.Interface
{
	public interface IRequestExecutorProvider
	{
		IRequestExecutor Create(string type);
	}
}
