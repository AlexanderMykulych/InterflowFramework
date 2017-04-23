using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterflowFrameworkRunner.TestProvider
{
	public class TestProvider
	{
		protected Dictionary<string, ITest> Tests = new Dictionary<string, ITest>();
		public void Add(string name, ITest test) {
			Tests.Add(name, test);
		}
		public void Execute(string testName) {
			var test = Tests.Where(x => x.Key == testName).Select(x => x.Value).FirstOrDefault();
			if (test != null) {
				test.Execute();
			} else {
				Console.WriteLine("No Test found!");
			}
		}
	}
}
