using InterflowFramework.Core.Message.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterflowFramework.Core.Message.Interface;
using Newtonsoft.Json.Linq;

namespace InterflowFramework.Core.Message.Model.Excell.Executor
{
	public class ExcellRequestExecutor : BaseRequestExecutor
	{
		public override IEnumerable<object> GetMathes(string request, IMessage message)
		{
			if (message is ExcellMessage)
			{
				var excellMessage = (ExcellMessage)message;
				var workbook = excellMessage.Workbook;
				if (workbook != null)
				{
					var firstPage = workbook.Worksheets.FirstOrDefault();
					if (firstPage != null)
					{
						var firstRow = firstPage.Rows().FirstOrDefault();
						if(firstRow != null) {
							return firstRow.Cells().Select(x => x.GetValue<string>());
						}
					}
				}
			}
			return new List<object>();
		}

		protected override IMessageRequestResponse CreateResponse()
		{
			return new ExcellMessageRequestResponse();
		}
	}
}
