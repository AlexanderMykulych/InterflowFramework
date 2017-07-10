using InterflowFramework.Core.Channel.Identified;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternalFramework
{
	public class MessagePackage: IIdentified
	{
		public string Id;
		public Object Object;
		public object AdditionalData;
		public MessagePackage(Object obj, string id) {
			Object = obj;
			Id = id;
		}

		public string GetId()
		{
			return Id;
		}
		public void SetAdditionalInfo(object data) {
			AdditionalData = data;
		}
	}
}
