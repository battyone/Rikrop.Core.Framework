using System;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;

namespace Rikrop.Core.Framework
{
    public class RepeatableExecutor
    {
        private static readonly Task CompletedTask = TaskEx.FromResult(true);
        private readonly Func<Task> _repeatedFunc;
        private readonly ITimer _timer;

        private bool _isExecutionStarted;

        public RepeatableExecutor(Func<Task> repeatedFunc, ITimer timer)
        {
            Contract.Requires<ArgumentNullException>(repeatedFunc != null);

            _repeatedFunc = repeatedFunc;
            _timer = timer;
            _timer.Tick += TimerOnElapsed;
        }

        public RepeatableExecutor(Func<Task> repeatedFunc, TimeSpan betweenExecuteTimeout)
            :this(repeatedFunc, new SimpleTimer{Interval = betweenExecuteTimeout})
        {
            Contract.Requires<ArgumentException>(betweenExecuteTimeout != TimeSpan.Zero);
        }

        public RepeatableExecutor(Action repeatedAction, TimeSpan betweenExecuteTimeout)
            : this(() => SyncExecuteRepeatedAction(repeatedAction), betweenExecuteTimeout)
        {
        }

        public RepeatableExecutor(Action repeatedAction, ITimer timer)
            : this(() => SyncExecuteRepeatedAction(repeatedAction), timer)
        {
        }

        private static Task SyncExecuteRepeatedAction(Action repeatedAction)
        {
            repeatedAction();

            return CompletedTask;
        }

        public void Start()
        {
            if (_isExecutionStarted)
            {
                return;
            }
            _isExecutionStarted = true;

            TaskEx.Run(() => TimerOnElapsed(_timer, null));
        }

        public void Stop()
        {
            _timer.Stop();
            _isExecutionStarted = false;
        }

        private async void TimerOnElapsed(object sender, EventArgs args)
        {
            _timer.Stop();

            try
            {
                await _repeatedFunc();
            }
            finally
            {
                if (_isExecutionStarted)
                {
                    _timer.Start();
                }
            }
        }
    }
}