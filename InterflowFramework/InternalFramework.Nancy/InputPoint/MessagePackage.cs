using InterflowFramework.Core.Channel.Identified;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternalFramework.Nancy.InputPoint
{
	public class MessagePackage: IIdentified
	{
		public string Id;
		public Object Object;
		public MessagePackage(Object obj, string id) {
			Object = obj;
			Id = id;
		}

		public string GetId()
		{
			return Id;
		}
	}
}
