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

namespace InterflowFrameworkRunner
{
	class Program
	{
		static void Main(string[] args)
		{
			var testProvider = new TestProvider.TestProvider();
			testProvider.Add("text", new TestTextImport());
			testProvider.Add("html", new HtmlTextImport());
			testProvider.Add("xml", new XmlTextImport());
			testProvider.Add("json", new JsonTextImport());
			testProvider.Add("excel", new ExcellTextImport());
			Console.WriteLine("Select mode:");
			var testName = Console.ReadLine();
			Console.WriteLine("Go");
			testProvider.Execute(testName);
			Console.WriteLine("End");
			Console.ReadKey();
		}
	}
}
