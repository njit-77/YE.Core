using System;
using System.Collections.Concurrent;
using System.Threading;

namespace YE.Core.Utility
{
    /// <summary>
    /// 简单生产者-消费者模式
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ProducerConsumer<T> where T : new()
    {
        /// <summary>队列</summary>
        private BlockingCollection<T> _queue = new();

        /// <summary>消费者待执行任务</summary>
        private readonly Action<T> _action;

        /// <summary>后台线程</summary>
        private readonly System.Threading.Thread _work;

        /// <summary>任务取消信号</summary>
        private readonly CancellationTokenSource _stopTokenSource = new();

        public ProducerConsumer(Action<T> action)
        {
            _action = action;

            _work = new Thread(DoWork)
            {
                IsBackground = true
            };
            _work.Start();
        }

        private void DoWork()
        {
            while (!_stopTokenSource.Token.IsCancellationRequested)
            {
                if (_queue.TryTake(out var item))
                {
                    _action(item);
                }
            }
        }

        public void AddT(T item)
        {
            _queue.Add(item);
        }

        public void Stop()
        {
            _stopTokenSource.Cancel();
        }

    }
}
