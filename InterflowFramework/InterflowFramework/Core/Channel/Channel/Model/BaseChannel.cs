using InterflowFramework.Core.Channel.InputPoint;
using InterflowFramework.Core.Channel.OutputPoint;
using InterflowFramework.Core.Channel.Subscriber.Interface;
using InterflowFramework.Core.Channel.Transport;
using InterflowFramework.Core.Channel.Transport.Const;
using InterflowFramework.Core.LinqExtension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterflowFramework.Core.Channel.Subscriber.Model;

namespace InterflowFramework.Core.Channel.Model
{
	public abstract class BaseChannel: IChannel, IDisposable, ISubscriber
	{
		public ITransport Transport;
		public IEnumerable<IInputPoint> InputPoints;
		public IEnumerable<IOutputPoint> OutputPoints;

		private bool _isConfigurated = false;

		private IExecutorSubscriber _subscriber;
		protected virtual IExecutorSubscriber Subscriber {
			get {
				if(_subscriber == null) {
					_subscriber = new BaseSubscriber();
				}
				return _subscriber;
			}
		}
		public BaseChannel() {
		}
		public BaseChannel(ITransport transport, IEnumerable<IInputPoint> inputPoints, IEnumerable<IOutputPoint> outputPoints) {
			Transport = transport;
			InputPoints = inputPoints;
			OutputPoints = outputPoints;
			Configurate();
		}
		public virtual void Enable()
		{
			Configurate();
			Transport.Enable();
			InputPoints.ForEach(x => x.Enable());
			OutputPoints.ForEach(x => x.Enable());
		}
		public virtual void Disable()
		{
			Transport.Disable();
			InputPoints.ForEach(x => x.Disable());
			OutputPoints.ForEach(x => x.Disable());
		}
		public virtual void Dispose()
		{
			Transport.Dispose();
			Transport = null;
			InputPoints.ForEach(point => point.Dispose());
			InputPoints = null;
			OutputPoints.ForEach(point => point.Dispose());
			OutputPoints = null;
		}
		protected virtual void Configurate() {
			if(_isConfigurated) {
				return;
			}
			ConfigurateTransport();
			ConfigurateInputPoints();
			ConfigurateOutputPoints();
			_isConfigurated = true;
		}
		protected virtual void ConfigurateOutputPoints()
		{
			if (OutputPoints != null)
			{
				OutputPoints.ForEach(point => ConfigurateOutputPointSubscribe(point));
			}
		}
		protected virtual void ConfigurateInputPoints()
		{
			if (InputPoints != null)
			{
				InputPoints.ForEach(point => ConfigurateInputPointSubscribe(point));
			}
		}
		protected abstract void ConfigurateInputPointSubscribe(IInputPoint point);
		protected abstract void ConfigurateOutputPointSubscribe(IOutputPoint point);
		protected abstract void ConfigurateTransport();

		public virtual ISubscriber On(string key, Action<object> onPublish)
		{
			Subscriber.On(key, onPublish);
			return this;
		}

		public virtual ISubscriber Unsubscribe(string key)
		{
			Subscriber.Unsubscribe(key);
			return this;
		}

		public virtual void Unsubscribe()
		{
			Subscriber.Unsubscribe();
		}
	}
}
