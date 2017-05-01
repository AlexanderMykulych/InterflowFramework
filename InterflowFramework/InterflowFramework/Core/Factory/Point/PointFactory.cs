using InterflowFramework.Core.Channel.InputPoint;
using InterflowFramework.Core.Channel.OutputPoint;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterflowFramework.Core.Factory.PointFactory
{
	public static class PointFactory
	{
		private static ConcurrentDictionary<string, List<Func<IInputPoint>>> _factoryListIn = new ConcurrentDictionary<string, List<Func<IInputPoint>>>();
		private static ConcurrentDictionary<string, List<IInputPoint>> _pointListIn = new ConcurrentDictionary<string, List<IInputPoint>>();
		private static ConcurrentDictionary<string, List<Func<IOutputPoint>>> _factoryListOut = new ConcurrentDictionary<string, List<Func<IOutputPoint>>>();
		private static ConcurrentDictionary<string, List<IOutputPoint>> _pointListOut = new ConcurrentDictionary<string, List<IOutputPoint>>();
		public static void InPoint(string name, Func<IInputPoint> creator)
		{
			if (!_factoryListIn.ContainsKey(name))
			{
				_factoryListIn.TryAdd(name, new List<Func<IInputPoint>>() { creator });
				return;
			}
			List<Func<IInputPoint>> list;
			if (_factoryListIn.TryGetValue(name, out list))
			{
				list.Add(creator);
			}
		}
		public static void OutPoint(string name, Func<IOutputPoint> creator)
		{
			if (!_factoryListOut.ContainsKey(name))
			{
				_factoryListOut.TryAdd(name, new List<Func<IOutputPoint>>() { creator });
				return;
			}
			List<Func<IOutputPoint>> list;
			if (_factoryListOut.TryGetValue(name, out list))
			{
				list.Add(creator);
			}
		}
		public static void PushIn(string name, object message) {
			var points = GetInPoints(name);
			if(points != null) {
				points.ForEach(point => SafePush(point, message));
			}
		}
		public static List<IInputPoint> GetInPoints(string name) {
			if(_pointListIn.ContainsKey(name)) {
				List<IInputPoint> points;
				if(_pointListIn.TryGetValue(name, out points)) {
					return points;
				}
			} else if(_factoryListIn.ContainsKey(name)) {
				List<Func<IInputPoint>> creators;
				if(_factoryListIn.TryGetValue(name, out creators)) {
					var result = creators.Select(creator =>
					{
						try
						{
							return creator.Invoke();
						}
						catch (Exception e)
						{
							//TODO: logger
						}
						return null;
					}).Where(point => point != null).ToList();
					_pointListIn.TryAdd(name, result);
					return result;
				}
			}
			return null;
		}
		//TODO: Add to mode of factory point: singleton and default
		public static List<IOutputPoint> GetOutPoints(string name)
		{
			if (_pointListOut.ContainsKey(name))
			{
				List<IOutputPoint> points;
				if (_pointListOut.TryGetValue(name, out points))
				{
					return points;
				}
			}
			else if (_factoryListOut.ContainsKey(name))
			{
				List<Func<IOutputPoint>> creators;
				if (_factoryListOut.TryGetValue(name, out creators))
				{
					var result = creators.Select(creator =>
					{
						try
						{
							return creator.Invoke();
						}
						catch (Exception e)
						{
							//TODO: logger
						}
						return null;
					}).Where(point => point != null).ToList();
					_pointListOut.TryAdd(name, result);
					return result;
				}
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
