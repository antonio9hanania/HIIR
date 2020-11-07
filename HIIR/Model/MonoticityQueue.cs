using System;
using System.Linq;
using HIIR.Model;

namespace HIIR.Model
{
    public class MonoticityQueue
    {
        private LimitedConcurrentQueue<double> SpeedQueue;
        private LimitedConcurrentQueue<bool> IsMonotonicIncQueue;
        private int _limit;
        public MonoticityQueue(int limit)
        {
            SpeedQueue = new LimitedConcurrentQueue<double>(limit);
            IsMonotonicIncQueue = new LimitedConcurrentQueue<bool>(limit);
            Limit = limit;
        }
        public int Limit
        {
            get { return _limit; }
            set
            {
                SpeedQueue.Limit = value;
                IsMonotonicIncQueue.Limit = value;
                _limit = value;
            }
        }
        public int ErrorAreaInKPH { get; set; } = 0;
        public bool IsMonotonicallyInc { get; private set; } = true;
        public void Enqueue(Double element)
        {
            // I need a sequence of [Limit] Trues to turn it's value to true
            if (!SpeedQueue.IsEmpty)
            {
                if (SpeedQueue.Max() <= element + ErrorAreaInKPH) // if the maximum value in the partial graph of speed graph is monotonic increase or in an acceptable error area of max, so its monotonic increase for running
                {
                   IsMonotonicallyInc = true;
                }
                else
                {
                    IsMonotonicallyInc = false;
                    
                }
            }

            SpeedQueue.Enqueue(element);
        }



    }
}
