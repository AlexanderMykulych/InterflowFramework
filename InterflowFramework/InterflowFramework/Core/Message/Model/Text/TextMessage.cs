using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterflowFramework.Core.Message.Interface;
using InterflowFramework.Core.Message.Model.Base;

namespace InterflowFramework.Core.Message.Model.Text
{
	public class TextMessage : BaseMessage
	{
		public string Text;
		public TextMessage(string initText) {
			Text = initText;
		}
		public override bool ValidateRequest(IMessageRequest request)
		{
			return request is TextMessageRequest;
		}
	}
}
