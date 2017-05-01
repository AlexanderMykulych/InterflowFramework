using InterflowFramework.Core.Channel.InputPoint.Const;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace InterflowFramework.Core.Channel.InputPoint.Model
{
	public class LoadTestInputPoint : BaseInputPoint
	{
		private System.Timers.Timer Timer;
		public LoadTestInputPoint(double interval) {
			Timer = new System.Timers.Timer();
			Timer.Elapsed += Timer_Elapsed;
			Timer.Interval = interval;
		}

		private void Timer_Elapsed(object sender, ElapsedEventArgs e)
		{
			Push(Thread.CurrentThread.ManagedThreadId + ": " + DateTime.Now.ToLongTimeString());
		}

		public override void Push(object message)
		{
			Subscriber.Execute(InputPointEvent.onMessage, message);
		}
		public override void Enable()
		{
			base.Enable();
			Timer.Enabled = true;
		}
		public override void Disable()
		{
			base.Disable();
			Timer.Enabled = false;
		}
		public override void Dispose()
		{
			base.Dispose();
			Timer.Dispose();
		}
	}
}
