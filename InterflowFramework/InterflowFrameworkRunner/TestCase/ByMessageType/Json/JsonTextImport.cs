using InterflowFramework.Core.Message;
using InterflowFramework.Core.Message.Interface;
using InterflowFramework.Core.Message.Model.Html;
using InterflowFramework.Core.Message.Model.Json;
using InterflowFrameworkRunner.TestProvider;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterflowFrameworkRunner.TestCase.ByMessageType.JsonTextImport
{
	class JsonTextImport : ITest
	{
		IMessageRequestProvider requestProvider = new JsonMessageRequestProvider();
		public void Execute()
		{
			var text = GetInputText();
			var message = GetMessage(text);
			var request1 = GetTestMessageRequest();
			var response = message.ExecuteRequest(request1);
			var textResponse = response as JsonMessageRequestResponse;
			if (textResponse != null)
			{
				textResponse
					.GetResponse()
					.Where(x => x != null)
					.All(x =>
					{
						Console.WriteLine(x);
						return true;
					});
			}
		}
		protected string GetInputText()
		{
			using (var reader = new StreamReader(new FileStream(@"C:\Dev\All Stuff\InterflowFramework\InterflowFramework\InterflowFrameworkRunner\TestCase\ByMessageType\Json\inputText.txt", FileMode.Open)))
			{
				return reader.ReadToEnd();
			}
		}
		protected IMessage GetMessage(string text)
		{
			var provider = new JsonMessageProvider();
			return provider.Create(text);
		}
		protected IMessageRequest GetTestMessageRequest()
		{
			return requestProvider.CreateRequest(@"jpaths:test.a1");
		}
	}
}
