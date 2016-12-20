using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkStateRouting
{
    class LSP
    {
        public int SourceId { get; }
        public int SequenceId { get; }
        public int TTL { get; set; }
        public Dictionary<int, int> Links { get; set; }

        public LSP(int source, int sequence, Dictionary<int, int> reachableNet)
        {
            SourceId = source;
            SequenceId = sequence;
            TTL = 10;
            Links = reachableNet;
        }

        public bool Newer(LSP old)
        {
            if (old == null || this.SequenceId > old.SequenceId)
            {
                return true;
            }
            return false;
        }

        public bool ConnectivityChanged(LSP old)
        {
            if (old == null)
            {
                return true;
            }
            foreach (var item in this.Links)
            {
                int val;
                bool exists = old.Links.TryGetValue(item.Key, out val);
                if (!exists || val != item.Value)
                {
                    return false;
                }
            }
            foreach (var item in old.Links)
            {
                int val;
                bool exists = this.Links.TryGetValue(item.Key, out val);
                if (!exists || val != item.Value)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
