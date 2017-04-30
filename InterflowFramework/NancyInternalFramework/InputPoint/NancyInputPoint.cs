using InterflowFramework.Core.Channel.InputPoint.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NancyInternalFramework.InputPoint
{
	public class NancyInputPoint : BaseInputPoint
	{
		public NancyInputPointConfig Config;
		private bool _isNancyConfigurated = false;
		public NancyInputPoint(NancyInputPointConfig config) {
			Config = config;
		}
		public override void Enable()
		{
			base.Enable();
			if(!_isNancyConfigurated) {
				ConfigurateNancy();
				_isNancyConfigurated = true;
			}
		}
		public void ConfigurateNancy() {

		}
		public override void Push(object message)
		{
			
		}
	}
}
