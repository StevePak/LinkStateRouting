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
        public List<int> ReachableNetwork { get; set; }

        public LSP(int source, int sequence, List<int> reachableNet)
        {
            SourceId = source;
            SequenceId = sequence;
            TTL = 10;
            ReachableNetwork = reachableNet;
        }
    }
}
