namespace PacketDotNet
{
    using PacketDotNet.LLDP;
    using System;
    using System.Collections.ObjectModel;

    public class TLVCollection : Collection<TLV>
    {
        private static readonly ILogInactive log;

        protected override void InsertItem(int index, TLV item)
        {
            if ((this.Count == 0) && (item.Type != TLVTypes.EndOfLLDPU))
            {
                base.InsertItem(0, new EndOfLLDPDU());
            }
            else if ((this.Count != 0) && (item.Type == TLVTypes.EndOfLLDPU))
            {
                this.SetItem(this.Count - 1, item);
                return;
            }
            int num = (this.Count != 0) ? (this.Count - 1) : 0;
            base.InsertItem(num, item);
        }
    }
}

