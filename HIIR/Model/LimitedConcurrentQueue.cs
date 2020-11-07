using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace HIIR.Model
{
    public class LimitedConcurrentQueue<ELEMENT> : ConcurrentQueue<ELEMENT>
    {
        private int _limit;

        public LimitedConcurrentQueue(int limit)
        {
            Limit = limit;
        }
        public int Limit { get { return _limit; } set { _limit = value; } }



        public new void Enqueue(ELEMENT element)
        {

            base.Enqueue(element);
            if (Count > Limit)
            {
                TryDequeue(out ELEMENT discard);
            }
        }
    }
}
