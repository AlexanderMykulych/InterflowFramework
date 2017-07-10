using InterflowFramework.Core.Channel.Packer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterflowFramework.Core.Channel.Packer.Model
{
	public class PackerStackOr : PackerStack
	{
		public IPacker _currentPacker;
		public override object Pack(object message)
		{
			return _currentPacker.Pack(message);
		}
		public override bool Valide(object obj)
		{
			_currentPacker = this.FirstOrDefault(p => p.Valide(obj));
			return _currentPacker != null;
		}
	}
}
