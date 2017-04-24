using InterflowFramework.Core.Message.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterflowFramework.Core.Message.Interface;

namespace InterflowFramework.Core.Message.Model.Excell
{
	public class ExcellMessageRequest : BaseMessageRequest
	{
		private IRequestExecutorProvider _executorProvider;
		protected override IRequestExecutorProvider ExecutorProvider {
			get {
				if (_executorProvider == null)
				{
					_executorProvider = new ExcellRequestExecutorProvider();
				}
				return _executorProvider;
			}
		}
	}
}
