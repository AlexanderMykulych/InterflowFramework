using InterflowFramework.Core.Channel.Packer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterflowFramework.Core.Channel.Packer.Model
{
	public abstract class BasePacker : IPacker
	{
		protected Type _packedType;

		public abstract object Pack(object message);

		public virtual bool Valide(object obj) {
			if(_packedType == null) {
				return false;
			}
			return _packedType.IsAssignableFrom(obj.GetType());
		}
	}
}
