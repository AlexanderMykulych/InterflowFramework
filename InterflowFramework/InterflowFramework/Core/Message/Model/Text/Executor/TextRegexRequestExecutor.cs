using System;
using InterflowFramework.Core.Message.Interface;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using InterflowFramework.Core.Message.Model.Base;

namespace InterflowFramework.Core.Message.Model.Text
{
	internal class TextRegexRequestExecutor: BaseRequestExecutor
	{
		public override IEnumerable<object> GetMathes(string request, IMessage requestedObject)
		{
			if (requestedObject is TextMessage)
			{
				string text = ((TextMessage)requestedObject).Text;
				var regex = new Regex(request);
				var matches = regex.Matches(text);
				foreach (Match match in matches)
				{
					yield return match.Value;
				}
			}
		}

		protected override IMessageRequestResponse CreateResponse()
		{
			return new TextMessageRequestResponse();
		}
	}
}