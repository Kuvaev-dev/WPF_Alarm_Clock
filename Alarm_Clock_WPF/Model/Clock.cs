using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Alarm_Clock_WPF
{
    public static class Clock
    {
        private static readonly Timer timer = new Timer(Tick, null, 0, 10);
        private static void Tick(object state)
        {
            Time = DateTime.Now;
            TimeChanged?.Invoke(null, EventArgs.Empty);
        }
        public static event EventHandler TimeChanged;
        public static DateTime Time { get; private set; }
    }
}
