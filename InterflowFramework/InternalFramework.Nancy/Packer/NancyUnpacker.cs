using InterflowFramework.Core.Channel.Packer.Interface;
using InterflowFramework.Core.Channel.Packer.Model;
using InternalFramework.Nancy.InputPoint;
using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternalFramework.Nancy.Packer
{
	public class NancyUnpacker : BasePacker
	{
		public NancyUnpacker() {
			_packedType = typeof(MessagePackage);
		}
		public override object Pack(object message)
		{
			var package = (MessagePackage)message;
			if (package.Object is NancyResponse)
			{
				var response = (NancyResponse)package.Object;
				package.Object = new Response()
				{
					ContentType = response.ContentType,
					Contents = response.GetContentStreamAction()
				};
			}
			return package;
		}
	}
}
