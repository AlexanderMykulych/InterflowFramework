using InterflowFramework.Core.Channel.Model;
using InterflowFramework.Core.Channel.Packer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterflowFramework.Core.Channel.Channel.Model
{
	public class PackedMessageChannel: MessageChannel
	{
		private IPacker _inToTrasport;
		private IPacker _outToTransport;
		private IPacker _transportToOut;
		private IPacker _transportToIn;
		public PackedMessageChannel InToTransportPacker(IPacker packer) {
			_inToTrasport = packer;
			return this;
		}
		public PackedMessageChannel OutToTransportPacker(IPacker packer) {
			_outToTransport = packer;
			return this;
		}
		public PackedMessageChannel TransportToOutPacker(IPacker packer)
		{
			_transportToOut = packer;
			return this;
		}
		public PackedMessageChannel TransportToInPacker(IPacker packer)
		{
			_transportToIn = packer;
			return this;
		}
		public PackedMessageChannel SetPackagers(IPacker inToTrasport, IPacker transportToOut, IPacker outToTransport, IPacker transportToIn) {
			return this
				.InToTransportPacker(inToTrasport)
				.OutToTransportPacker(outToTransport)
				.TransportToOutPacker(transportToOut)
				.TransportToInPacker(transportToIn);
		}
		protected override void onInputPointMessage(object obj)
		{
			PackAndSend(obj, _inToTrasport, base.onInputPointMessage);
		}
		protected override void onOutputPointResponse(object obj)
		{
			PackAndSend(obj, _outToTransport, base.onOutputPointResponse);
		}
		protected override void OnTransportMessage(object obj)
		{
			PackAndSend(obj, _transportToOut, base.OnTransportMessage);
		}
		protected override void OnTransportResponse(object obj)
		{
			PackAndSend(obj, _transportToIn, base.OnTransportResponse);
		}

		protected virtual bool PackAndSend(object obj, IPacker packer, Action<object> action) {
			if(packer == null) {
				action(obj);
				return true;
			}
			object package = null;
			if (packer != null && packer.Valide(obj))
			{
				package = packer.Pack(obj);
			}
			if (package != null)
			{
				action(package);
				return true;
			}
			return false;
		}
	}
}
