using InterflowFramework.Core.Message.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterflowFramework.Core.Message.Interface;
using Newtonsoft.Json.Linq;
using ClosedXML.Excel;
using System.IO;

namespace InterflowFramework.Core.Message.Model.Excell
{
	public class ExcellMessage : BaseMessage
	{
		public XLWorkbook Workbook;
		public ExcellMessage(Stream stream) {
			Workbook = new XLWorkbook(stream);
		}
		public ExcellMessage() {
			Workbook = new XLWorkbook();
		}
		public override bool ValidateRequest(IMessageRequest request)
		{
			return request is ExcellMessageRequest;
		}
	}
}
