using InterflowFramework.Core.Channel;
using InterflowFramework.Core.Channel.InputPoint;
using InterflowFramework.Core.Channel.Model;
using InterflowFramework.Core.Channel.OutputPoint;
using InterflowFramework.Core.Channel.Transport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterflowFramework.Core.Factory.Channel
{
	public abstract class ChannelCreator<T, Creator> where T: IChannel where Creator: ChannelCreator<T,Creator>
	{
		protected string _name;
		protected List<IInputPoint> _inPoints;
		public List<IInputPoint> InPoints {
			get {
				if(_inPoints == null) {
					_inPoints = new List<IInputPoint>();
				}
				return _inPoints;
			}
		}
		protected List<IOutputPoint> _outPoints;
		public List<IOutputPoint> OutPoints {
			get {
				if (_outPoints == null)
				{
					_outPoints = new List<IOutputPoint>();
				}
				return _outPoints;
			}
		}
		protected ITransport _transport;
		public Creator Is(string name, bool singleton = true) {
			_name = name;
			return (Creator)this;
		}
		public Creator In(string name)
		{
			var points = PointFactory.PointFactory.GetInPoints(name);
			if(points != null && points.Any()) {
				InPoints.AddRange(points);
			}
			return (Creator)this;
		}
		public Creator In(IInputPoint point)
		{
			if (point != null)
			{
				InPoints.Add(point);
			}
			return (Creator)this;
		}
		public Creator Out(string name)
		{
			var points = PointFactory.PointFactory.GetOutPoints(name);
			if (points != null && points.Any())
			{
				OutPoints.AddRange(points);
			}
			return (Creator)this;
		}
		public Creator Out(IOutputPoint point)
		{
			if (point != null)
			{
				OutPoints.Add(point);
			}
			return (Creator)this;
		}
		public Creator InOut(string name)
		{
			return In(name).Out(name);
		}
		public Creator Transport(string name) {
			return (Creator)this;
		}
		public Creator Transport(ITransport transport) {
			_transport = transport;
			return (Creator)this;
		}
		public Creator Serializer(string name) {
			return (Creator)this;
		}
		public abstract IChannel Create();
	}
}
