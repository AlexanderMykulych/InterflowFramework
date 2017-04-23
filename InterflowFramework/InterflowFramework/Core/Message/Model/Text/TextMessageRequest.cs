using InterflowFramework.Core.Message.Interface;
using InterflowFramework.Core.Message.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterflowFramework.Core.Message.Model.Text
{
	public class TextMessageRequest : BaseMessageRequest
	{
		private IRequestExecutorProvider _executorProvider;
		protected override IRequestExecutorProvider ExecutorProvider {
			get {
				if(_executorProvider == null) {
					_executorProvider = new TextRequestExecutorProvider();
				}
				return _executorProvider;
			}
		}
	}
}
