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
	public class ChannelCreator
	{
		private string _name;
		private List<IInputPoint> _inPoints;
		public List<IInputPoint> InPoints {
			get {
				if(_inPoints == null) {
					_inPoints = new List<IInputPoint>();
				}
				return _inPoints;
			}
		}
		private List<IOutputPoint> _outPoints;
		public List<IOutputPoint> OutPoints {
			get {
				if (_outPoints == null)
				{
					_outPoints = new List<IOutputPoint>();
				}
				return _outPoints;
			}
		}
		private ITransport _transport;
		public ChannelCreator Is(string name, bool singleton = true) {
			_name = name;
			return this;
		}
		public ChannelCreator In(string name)
		{
			var points = PointFactory.PointFactory.GetInPoints(name);
			if(points != null && points.Any()) {
				InPoints.AddRange(points);
			}
			return this;
		}
		public ChannelCreator In(IInputPoint point)
		{
			if (point != null)
			{
				InPoints.Add(point);
			}
			return this;
		}
		public ChannelCreator Out(string name)
		{
			var points = PointFactory.PointFactory.GetOutPoints(name);
			if (points != null && points.Any())
			{
				OutPoints.AddRange(points);
			}
			return this;
		}
		public ChannelCreator Out(IOutputPoint point)
		{
			if (point != null)
			{
				OutPoints.Add(point);
			}
			return this;
		}
		public ChannelCreator InOut(string name)
		{
			return In(name).Out(name);
		}
		public ChannelCreator Transport(string name) {
			return this;
		}
		public ChannelCreator Transport(ITransport transport) {
			_transport = transport;
			return this;
		}
		public ChannelCreator Serializer(string name) {
			return this;
		}
		public IChannel Create() {
			return new MessageChannel()
			{
				InputPoints = _inPoints,
				OutputPoints = _outPoints,
				Transport = _transport
			};
		}
	}
}
