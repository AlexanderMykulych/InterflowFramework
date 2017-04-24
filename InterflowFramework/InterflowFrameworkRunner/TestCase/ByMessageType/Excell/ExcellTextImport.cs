using InterflowFramework.Core.Message;
using InterflowFramework.Core.Message.Interface;
using InterflowFramework.Core.Message.Model.Excell;
using InterflowFramework.Core.Message.Model.Html;
using InterflowFramework.Core.Message.Model.Json;
using InterflowFrameworkRunner.TestProvider;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterflowFrameworkRunner.TestCase.ByMessageType.ExcellTextImport
{
	class ExcellTextImport : ITest
	{
		IMessageRequestProvider requestProvider = new ExcellMessageRequestProvider();
		public void Execute()
		{
			var stream = GetInputText();
			IMessage message;
			try
			{
				message = GetMessage(stream);
			}
			finally
			{
				stream.Dispose();
			}
			var request1 = GetTestMessageRequest();
			var response = message.ExecuteRequest(request1);
			var textResponse = response as ExcellMessageRequestResponse;
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
		protected Stream GetInputText()
		{
			return new FileStream(@"C:\Dev\All Stuff\InterflowFramework\InterflowFramework\InterflowFrameworkRunner\TestCase\ByMessageType\Excell\input.xlsx", FileMode.Open);
		}
		protected IMessage GetMessage(Stream stream)
		{
			var provider = new ExcellMessageProvider();
			return provider.Create(stream);
		}
		protected IMessageRequest GetTestMessageRequest()
		{
			return requestProvider.CreateRequest(@"excel");
		}
	}
}
