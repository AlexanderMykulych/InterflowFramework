using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NancyInternalFramework.InputPoint
{
	public class NancyInputPointConfig
	{
		public NancyInputType Type;
		public string Route;
		NancyInputPointConfig(NancyInputType type, string route) {
			Type = type;
			Route = route;
		}
	}
	public enum NancyInputType {
		json,
		xml,
		text
	}
}
