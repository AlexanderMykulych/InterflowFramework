using InterflowFramework.Core.Channel.Transport.Model.Inline;
using InterflowFramework.Core.Factory.Channel;
using InterflowFramework.Core.Factory.PointFactory;
using InterflowFramework.RabbitMq.Transport;
using InternalFramework.Nancy.InputPoint;
using InternalFramework.Nancy.OutputPoint;
using InternalFramework.Nancy.Packer;
using Nancy;
using Nancy.Hosting.Self;
using Nancy.Responses;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MimeMapping;
using InternalFramework;
using InternalFramework.OutputPoint;

namespace _2_Browser_Nancy_RabbitMq
{
	class Program
	{
		static void Main(string[] args)
		{
			new PointFactory()
				._In("nancy_blog", () => new NancyInputPoint("Get", "/blog/{id}", null, false))
				._In("nancy_file", () => new NancyInputPoint("Get", "/Files/{fileType}/{fileName}", null, true))
				._Out("nancy_file", () => new ResponseOutputPoint(GetFile));

			var rabbitChannelCreator = new PackedMessageChannelCreator()
				.In("nancy_blog")
				.Packers(responseUnpacker: new NancyUnpacker())
				.Transport(new RabbitMqTransport("host=localhost", "demo2_first", true, false, true));

			var fileChannelCreator = new MessageChannelCreator()
				.InOut("nancy_file")
				.Transport(new InlineTransport());


			using (var host = new NancyHost(new Uri("http://localhost:8888/nancy/")))
			{
				host.Start();
				using (var rabbitChannel = rabbitChannelCreator.Create())
				using (var fileChannel = fileChannelCreator.Create())
				{
					rabbitChannel.Enable();
					fileChannel.Enable();

					Process.Start("http://localhost:8888/nancy/blog/1");
					Console.WriteLine("Press Any key to stop.");
					Console.ReadKey();
				}
			}
		}
		static object GetFile(object message)
		{
			if (message is MessagePackage)
			{
				var package = (MessagePackage)message;
				var requestData = package.Object as Dictionary<string, object>;
				var fileType = requestData["fileType"].ToString();
				var fileName = requestData["fileName"].ToString();
				var module = package.AdditionalData as NancyModule;
				if (module != null)
				{
					return new GenericFileResponse(GetFilePath(fileType + "/" + fileName), GetFileContentType(fileName), module.Context);
				}
			}
			return string.Empty;
		}
		static string GetFilePath(string fileName) {
			return string.Format(@"Files\{0}", fileName);
		}
		static string GetFileContentType(string fileName) {
			return MimeMapping.MimeTypes.GetMimeMapping(fileName);
		}
	}
}
