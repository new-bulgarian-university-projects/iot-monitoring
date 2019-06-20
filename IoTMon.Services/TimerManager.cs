using IoTMon.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IoTMon.Services
{
    public class TimerManager : ITimeManager, IDisposable
    {
        private Timer _timer;
        private AutoResetEvent _autoResetEvent;
        private Func<DateTime, Task<DateTime?>> func;

        public DateTime TimerStarted { get; set; }
        public DateTime? LastUpdated { get; set; } = null;

        public TimerManager(int period, Func<DateTime, Task<DateTime?>> func)
        {
            this.func = func;
            _autoResetEvent = new AutoResetEvent(false);
            _timer = new Timer(ExecuteAsync, _autoResetEvent, 0, period * 1000);
            TimerStarted = DateTime.Now;
        }

        public async void ExecuteAsync(object stateInfo)
        {
            this.LastUpdated = await func(this.LastUpdated ?? this.TimerStarted);
        }

        public void Dispose()
        {
            if (this._timer != null)
            {
                this._timer.Dispose();
            }
        }
    }
}
