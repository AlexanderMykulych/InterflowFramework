using InterflowFramework.Core.Channel.InputPoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterflowFramework.Core.Factory.Point
{
	public class PointCreateInfo<T>
	{
		public Func<T> Creator;
		public bool Singleton;
		private T _point;
		public virtual T Point {
			get {
				if(_point == null) {
					_point = Creator.Invoke();
				}
				return _point;
			}
		}
		public virtual T GetPoint() {
			if(Singleton) {
				return Point;
			}
			return Creator.Invoke();
		}
	}
}
