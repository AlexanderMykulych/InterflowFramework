using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternalFramework.Nancy.InputPoint
{
	public class NancyResponse
	{
		public string ContentType;
		public string Content;

		internal Action<Stream> GetContentStreamAction()
		{
			return stream =>
			{
				var writer = new StreamWriter(stream, Encoding.UTF8);
				writer.Write(Content);
				writer.Flush();
			};
		}
	}
}
