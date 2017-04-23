using InterflowFramework.Core.Message.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterflowFramework.Core.Message.Model.Base
{
	public abstract class BaseRequestExecutorProvider : IRequestExecutorProvider
	{
		public virtual IRequestExecutor Create(string type)
		{
			return Executors.Where(x => x.Key == type).Select(x => x.Value).FirstOrDefault();
		}
		protected abstract Dictionary<string, IRequestExecutor> Executors {
			get;
		}
	}
}
