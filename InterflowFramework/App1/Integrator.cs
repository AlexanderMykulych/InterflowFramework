using InterflowFramework.Core.Channel.InputPoint;
using InterflowFramework.Core.Channel.InputPoint.Model;
using InterflowFramework.Core.Factory.Channel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1 {
	public class Integrator {
		public MessageChannelCreator Channel;
		public IInputPoint InputPoint;
		public IOutputPoint OutputPoint;
		public Integrator() {
			InputPoint = new SimpleInputPoint();
			OutputPoint = new 
			Channel = new MessageChannelCreator()
				.In(InputPoint)
				.Out();
		}
		public bool Integrate(Contact contact) {
			


			return true;
		}
	}
}
