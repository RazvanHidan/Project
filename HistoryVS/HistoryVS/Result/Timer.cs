using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace HistoryVS
{
    public class Options 
    {

        private readonly Stopwatch stopwatch;
        public Options()
        {
            stopwatch = new Stopwatch();

        }
        
        public void Start()
        {
            stopwatch.Restart();
        }

        public void Stop()
        {
            stopwatch.Stop();
        }

            public bool IsRunning
        {
            get { return stopwatch.IsRunning; }
        }

            public long MillisecondsElapsed
        {
            get { return stopwatch.ElapsedMilliseconds; }
        }

        public Options ContainedOptions
        {
            get
            {
                return this;
            }
        }
    }
}
