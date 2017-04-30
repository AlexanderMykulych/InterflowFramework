using InterflowFrameworkRunner.TestCase.ByMessageType.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterflowFrameworkRunner.TestProvider;
using InterflowFrameworkRunner.TestCase.ByMessageType.Html;
using InterflowFrameworkRunner.TestCase.ByMessageType.JsonTextImport;
using InterflowFrameworkRunner.TestCase.ByMessageType.ExcellTextImport;
using InterflowFramework.Core.Channel.OutputPoint.Model.TestPoint;
using InterflowFrameworkRunner.TestCase.Channel.BaseTestOnText;
using Nancy.Hosting.Self;

namespace InterflowFrameworkRunner
{
	class Program
	{
		static void Main(string[] args)
		{
			var testProvider = new TestProvider.TestProvider();
			var test = new TelegramTest();
			testProvider.Add("text", new TestTextImport());
			testProvider.Add("html", new HtmlTextImport());
			testProvider.Add("xml", new XmlTextImport());
			testProvider.Add("json", new JsonTextImport());
			testProvider.Add("excel", new ExcellTextImport());
			testProvider.Add("tel", test);
			Console.WriteLine("Select mode:");
			var testName = Console.ReadLine();
			Console.WriteLine("Go");
			testProvider.Execute(testName);
			Console.WriteLine("End");
			Console.ReadKey();
			test.Channel.Dispose();
		}
	}
}
