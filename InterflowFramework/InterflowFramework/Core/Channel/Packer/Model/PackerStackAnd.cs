using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterflowFramework.Core.Channel.Packer.Model
{
	public class PackerStackAnd : PackerStack
	{
		public override object Pack(object message)
		{
			object result = message;
			foreach(var packer in this) {
				result = packer.Pack(result);
			}
			return result;
		}

		public override bool Valide(object obj)
		{
			return this.All(packer => packer.Valide(obj));
		}
	}
}
