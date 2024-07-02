using System;
using System.Collections.Concurrent;
using System.Threading;

namespace YE.Core
{
    /// <summary>
    /// 简单生产者-消费者模式
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ProducerConsumer<T> where T : new()
    {
        /// <summary>队列</summary>
        private ConcurrentQueue<T> _queue;

        /// <summary>信号量</summary>
        private readonly EventWaitHandle _wait;

        /// <summary>消费者待执行任务</summary>
        private readonly Action<T> _action;

        /// <summary>后台线程</summary>
        private readonly System.Threading.Thread t_work;


        public ProducerConsumer(Action<T> action)
        {
            _queue = new ConcurrentQueue<T>();

            _wait = new AutoResetEvent(false);

            _action = action;

            t_work = new Thread(DoWork)
            {
                IsBackground = true
            };
            t_work.Start();
        }

        private void DoWork()
        {
            while (true)
            {
                bool is_get_data = false;
                T item = default;

                if (_queue.Count > 0)
                {
                    is_get_data = _queue.TryDequeue(out item);
                }

                if (is_get_data && item == null)
                {
                    /// 退出操作
                    return;
                }
                if (!is_get_data)
                {
                    _wait.WaitOne();
                }
                else
                {
                    _action(item);
                }
            }
        }

        public void AddT(T item)
        {
            _queue.Enqueue(item);
            _wait.Set();
        }
    }
}
