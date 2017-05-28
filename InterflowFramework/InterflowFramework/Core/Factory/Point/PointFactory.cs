using InterflowFramework.Core.Channel.InputPoint;
using InterflowFramework.Core.Channel.OutputPoint;
using InterflowFramework.Core.Factory.Point;
using InterflowFramework.Core.LinqExtension;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterflowFramework.Core.Factory.PointFactory
{
	public class PointFactory
	{
		private static ConcurrentDictionary<string, List<InPointCreateInfo>> _factoryListIn = new ConcurrentDictionary<string, List<InPointCreateInfo>>();
		private static ConcurrentDictionary<string, List<OutPointCreateInfo>> _factoryListOut = new ConcurrentDictionary<string, List<OutPointCreateInfo>>();
		public static void In(string name, Func<IInputPoint> creator, bool singleton = true) {
			var info = new InPointCreateInfo()
			{
				Creator = creator,
				Singleton = singleton
			};
			if (!_factoryListIn.ContainsKey(name))
			{
				_factoryListIn.TryAdd(name, new List<InPointCreateInfo>() { info });
				return;
			}
			List<InPointCreateInfo> list;
			if (_factoryListIn.TryGetValue(name, out list))
			{
				list.Add(info);
			}
		}
		public PointFactory _In(string name, Func<IInputPoint> creator, bool singleton = true)
		{
			In(name, creator, singleton);
			return this;
		}
		public PointFactory _Out(string name, Func<IOutputPoint> creator, bool singleton = true) {
			Out(name, creator, singleton);
			return this;
		}
		public static void Out(string name, Func<IOutputPoint> creator, bool singleton = true)
		{
			var info = new OutPointCreateInfo()
			{
				Creator = creator,
				Singleton = singleton
			};
			if (!_factoryListOut.ContainsKey(name))
			{
				_factoryListOut.TryAdd(name, new List<OutPointCreateInfo>() { info });
				return;
			}
			List<OutPointCreateInfo> list;
			if (_factoryListOut.TryGetValue(name, out list))
			{
				list.Add(info);
			}
		}
		public PointFactory _PushIn(string name, object message, Func<object, bool> validator = null)
		{
			PushIn(name, message, validator);
			return this;
		}
		public static void PushIn(string name, object message, Func<object, bool> validator = null)
		{
			var points = GetInPoints(name);
			if(points != null) {
				if (validator == null || validator(message))
				{
					points.ForEach(point => SafePush(point, message));
				}
			}
			return;
		}
		public static IEnumerable<IInputPoint> GetInPoints(string name = null) {
			if(!string.IsNullOrEmpty(name) && _factoryListIn.ContainsKey(name)) {
				List<InPointCreateInfo> pointCreators;
				if(_factoryListIn.TryGetValue(name, out pointCreators)) {
					return pointCreators
						.Select(x => x.GetPoint())
						.ToList();
				}
			} else if(string.IsNullOrEmpty(name)) {
				return _factoryListIn
					.SelectMany(x => x.Value.Select(y => y.GetPoint())).ToList();
			}
			return null;
		}
		public PointFactory _Enable(params string[] points)
		{
			Enable(points);
			return this;
		}
		public static void Enable(params string[] points) {
			points.ForEach(name => GetInPoints(name).ForEach(point => point.Enable()));
		}
		public static IEnumerable<IOutputPoint> GetOutPoints(string name = null)
		{
			if (!string.IsNullOrEmpty(name) && _factoryListOut.ContainsKey(name))
			{
				List<OutPointCreateInfo> pointCreators;
				if (_factoryListOut.TryGetValue(name, out pointCreators))
				{
					return pointCreators
						.Select(x => x.GetPoint()).ToList();
				}
			}
			else if (string.IsNullOrEmpty(name))
			{
				return _factoryListOut
					.SelectMany(x => x.Value.Select(y => y.GetPoint())).ToList();
			}
			return null;
		}
		private static void SafePush(IInputPoint point, object message) {
			try {
				point.Push(message);
			} catch(Exception e) {
				//TODO: logged
			}
		}
	}
}
