using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterflowFramework.Core.Message.Interface;
using InterflowFramework.Core.Message.Model.Base;
using ProtoBuf;

namespace InterflowFramework.Core.Message.Model.Text
{
	[ProtoContract]
	public class TextMessage : BaseMessage
	{
		[ProtoMember(1)]
		public string Text;
		public TextMessage() {
		}
		public TextMessage(string initText) {
			Text = initText;
		}
		public override bool ValidateRequest(IMessageRequest request)
		{
			return request is TextMessageRequest;
		}
		public override string ToString()
		{
			return Text;
		}
	}
}
