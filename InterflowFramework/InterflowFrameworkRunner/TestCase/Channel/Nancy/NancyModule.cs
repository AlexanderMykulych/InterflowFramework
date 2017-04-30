using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterflowFrameworkRunner.TestCase.Channel.Nancy
{
	public class TestNancyModule : NancyModule
	{
		public TestNancyModule(string modulePath) : base(modulePath)
		{
			Post("/test", msg =>
			{
				Console.WriteLine("nancy!");
				return "";
			});
		}
	}
}
