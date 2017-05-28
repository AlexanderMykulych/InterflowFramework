using _2_RabbitMq_Console.Context;
using _2_RabbitMq_Console.Model;
using InterflowFramework.Core.Factory.Channel;
using InterflowFramework.Core.Factory.PointFactory;
using InterflowFramework.RabbitMq.Transport;
using InternalFramework.Nancy.InputPoint;
using InternalFramework.Nancy.OutputPoint;
using Microsoft.EntityFrameworkCore;
using Nancy;
using Newtonsoft.Json.Linq;
using RazorEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2_RabbitMq_Console
{
	class Program
	{
		static BloggingContext Context;
		static void Main(string[] args)
		{
			PointFactory.Out("response", () => new ResponseOutputPoint(OnMessage));

			using (var channel = new MessageChannelCreator()
				.Transport(new RabbitMqTransport("host=localhost", "demo2_first", false, true))
				.Out("response")
				.Create()) {

				using(var context = InitDbContext()) {
					InitDataInDb(context);
					Context = context;
					channel.Enable();
					Console.WriteLine("Press Any key to stop.");
					Console.ReadKey();
				}
			}
		}
		public static object OnMessage(object message) {
			if(message is MessagePackage) {
				var dict = ((MessagePackage)message).Object as Dictionary<string, object>;
				string id = dict["id"].ToString();
				int idInt = 0;
				if(int.TryParse(id, out idInt)) {
					var blog = Context.Blogs.FirstOrDefault(x => x.Id == idInt);
					if (blog != null)
					{
						return new NancyResponse()
						{
							ContentType = "text/html",
							Content = ProcessTemplate("single", blog)
						};
					}
				} else {
					return new NancyResponse()
					{
						ContentType = "text/html",
						Content = ProcessTemplate("index", Context.Blogs.ToList())
					};
				}
			}
			return "Pong";
		}

		public static BloggingContext InitDbContext() {
			var options = new DbContextOptionsBuilder<BloggingContext>()
				.UseInMemoryDatabase(databaseName: "Add_writes_to_database")
				.Options;

			return new BloggingContext(options);
		}
		public static void InitDataInDb(BloggingContext context) {
			context.Blogs.Add(new Blog()
			{
				Id = 1,
				Title = "Перша стаття",
				Content = "Якийсь текст",
				Author = "Alex Mykulych",
				CreatedOn = DateTime.Now
			});
			context.Blogs.Add(new Blog()
			{
				Id = 2,
				Title = "Друга стаття",
				Content = "Якийсь текст 2",
				Author = "Alex Mykulych",
				CreatedOn = DateTime.Now.AddDays(-10)
			});
			context.SaveChanges();
		}
		public static string GetTemplate(string name) {
			string template = string.Empty;
			using (var stream = new StreamReader(new FileStream("../../Resources/" + name + ".html", FileMode.Open))) {
				template = stream.ReadToEnd();
			}
			return template;
		}
		public static string ProcessTemplate(string name, Object model) {
			return Razor.Parse(GetTemplate(name), model);
		}
	}
}
