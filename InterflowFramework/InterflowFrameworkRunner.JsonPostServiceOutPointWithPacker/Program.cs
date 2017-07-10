using InterflowFramework.Core.Channel.InputPoint.Model;
using InterflowFramework.Core.Channel.Transport.Model.Inline;
using InterflowFramework.Core.Factory.Channel;
using InterflowFramework.Core.Factory.PointFactory;
using InterflowFramework.HttpWeb.OutputPoint;
using InternalFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace InterflowFrameworkRunner.JsonPostServiceOutPointWithPacker
{
	class Program
	{
		static void Main(string[] args)
		{
			new PointFactory()
				._Out("jsonPost", () => new JsonPostServiceOutPoint("http://localhost:8183/Service"))
				._In("jsonPost", () => new SimpleWithResponseInputPoint(message => OnConsole(message)));

			using (var channel = new PackedMessageChannelCreator()
				.Packers(null, null, null, null)
				.InOut("jsonPost")
				.Transport(new InlineTransport())
				.Create())
			{
				channel.Enable();
				PointFactory.PushIn("jsonPost", new
				{
					a = 1,
					b = "abc"
				});
				Console.ReadKey();
			}
		}
		public static void OnConsole(object message)
		{
			var response = (message as MessagePackage).Object as HttpWebResponse;
			using (var reader = new System.IO.StreamReader(response.GetResponseStream(), ASCIIEncoding.ASCII))
			{
				string responseText = reader.ReadToEnd();
				Console.WriteLine(responseText);
			}
		}
	}
}
