using InterflowFramework.Core.Channel.Packer.Interface;
using InternalFramework.Nancy.InputPoint;
using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternalFramework.Nancy.Packer
{
	public class NancyUnpacker : IUnpacker
	{
		public object Unpack(object message)
		{
			if(message is MessagePackage) {
				var package = (MessagePackage)message;
				if(package.Object is NancyResponse) {
					var response = (NancyResponse)package.Object;
					package.Object = new Response()
					{
						ContentType = response.ContentType,
						Contents = response.GetContentStreamAction()
					};
				}
			}
			return message;
		}
	}
}
