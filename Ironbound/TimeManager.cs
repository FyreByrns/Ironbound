using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Ironbound {
    public static class TimeManager {
        static Timer timer = new Timer();
        public static DateTime GlobalDateTime => DateTime.Now;
        public static DateTime CurrentDateTime => Paused ? PausedDateTime : GlobalDateTime;
        public static DateTime PausedDateTime => pausedDateTime;
        public static ulong GlobalTime => globalTime;
        public static bool Paused {
            get => paused; set {
                paused = value;
                if (paused) pausedDateTime = GlobalDateTime;
            }
        }
        static ulong globalTime = 0;
        static bool paused = false;
        static DateTime pausedDateTime;

        static DateTime lastTime = DateTime.Now;

        static TimeManager() {
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        private static void Timer_Elapsed(object sender, ElapsedEventArgs e) {
            int elapsed = e.SignalTime.Subtract(lastTime).Milliseconds;
            if (!paused) globalTime += (ulong)elapsed;
            lastTime = e.SignalTime;
        }
    }
}
