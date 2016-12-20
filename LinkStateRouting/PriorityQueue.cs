using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkStateRouting
{
    class PriorityQueue<TValue, TPriority> where TPriority : IComparable<TPriority>
    {
        private Dictionary<TValue, TPriority> queue;

        public int Count { get { return queue.Count; } }

        public PriorityQueue()
        {
            queue = new Dictionary<TValue, TPriority>();
        }


        public void Enqueue(TValue v, TPriority p)
        {
            queue.Add(v, p);
        }

        public TValue Dequeue()
        {
            TValue t = queue.OrderBy(x => x.Value).First().Key;
            queue.Remove(t);
            return t;
        }

        public TValue Peek()
        {
            TValue t = queue.OrderBy(x => x.Value).First().Key;
            queue.Remove(t);
            return t;
        }

        public void ChangePriority(TValue v, TPriority p)
        {
            queue[v] = p;
        }
    }
}
