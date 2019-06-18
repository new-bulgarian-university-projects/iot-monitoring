using IoTMon.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace IoTMon.Services
{
    public class TimerManager : ITimeManager, IDisposable
    {
        private Timer _timer;
        private AutoResetEvent _autoResetEvent;
        private Action _action;

        public DateTime TimerStarted { get; }

        public TimerManager(Action action)
        {
            _action = action;
            _autoResetEvent = new AutoResetEvent(false);
            _timer = new Timer(Execute, _autoResetEvent, 0, 2000);
            TimerStarted = DateTime.Now;
        }

        public void Execute(object stateInfo)
        {
            _action();
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
